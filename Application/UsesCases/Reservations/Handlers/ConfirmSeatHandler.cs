using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.UsesCases.Reservations.Commands;
using Domain.Entities;
using Domain.Exeptions;

namespace Application.UsesCases.Reservations.Handlers
{
    public class ConfirmSeatHandler : IConfirmSeatHandler
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly IAuditLogRepository _auditLogRepository;

        public ConfirmSeatHandler(
            IReservationRepository reservationRepository,
            ISeatRepository seatRepository,
            IAuditLogRepository auditLogRepository)
        {
            _reservationRepository = reservationRepository;
            _seatRepository = seatRepository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<ConfirmSeatResponse> Handle(ConfirmSeatCommand command)
        {

            string userid="" ; 
            
            try
            {
                //  Buscar reserva con asientos
                var reservation = await _reservationRepository.GetByIdWithSeats(command.ReservationId);
                userid = reservation.UserId;

                if (reservation == null)
                    throw new BusinessException("Reservation not found");

                //  Validar estado
                if (reservation.Status != ReservationStatus.Pending)
                    throw new BusinessException("Invalid reservation state");

                //  Validar expiración
                if (reservation.ExpiresAt < DateTime.UtcNow)
                {
                    reservation.Status = ReservationStatus.Expired;
                    await _reservationRepository.UpdateAsync(reservation);

                    throw new BusinessException("Reservation expired");
                }

                //  Obtener TODOS los asientos
                var seats = reservation.Seats
                    .Select(rs => rs.SeatObj)
                    .ToList();

                if (seats.Count == 0)
                    throw new BusinessException("No seats in reservation");

                //  Actualizar TODOS los asientos
                foreach (var seat in seats)
                {
                    seat.Status = SeatStatus.Sold;
                }

                //  Confirmar reserva
                reservation.Status = ReservationStatus.Paid;

                //  Guardar todo
                await _reservationRepository.UpdateAsync(reservation);
               // await _seatRepository.UpdateRangeAsync(seats);

                //  Auditoría éxito
                await _auditLogRepository.AddAsync(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = reservation.UserId,
                    Action = "CONFIRM_SUCCESS",
                    EntityType = "Reservation",
                    EntityId = reservation.Id.ToString(),
                    Details = $"Reservation confirmed for seats: {string.Join(",", seats.Select(s => s.Id))}",
                    CreatedAt = DateTime.UtcNow
                });

                return new ConfirmSeatResponse
                {
                    ReservationId = reservation.Id,
                   // SeatIds = seats.Select(s => s.Id).ToList(),
                    Status = reservation.Status.ToString(),
                    ConfirmedAt = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                //  Auditoría fallo
                await _auditLogRepository.AddAsync(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = userid,
                    Action = "CONFIRM_FAILED",
                    EntityType = "Reservation",
                    EntityId = command.ReservationId.ToString(),
                    Details = ex.Message,
                    CreatedAt = DateTime.UtcNow
                });

                throw;
            }
        }
    }


}

