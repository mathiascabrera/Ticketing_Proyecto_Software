using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces;
using Application.UseCases.Reservations.Commands;
using Application.UsesCases.Reservations.Commands;
using Domain.Entities;
using Domain.Exeptions;

namespace Application.UseCases.Reservations.Handlers
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
            try
            {
                // 1. Buscar reserva
                var reservation = await _reservationRepository.GetByIdAsync(command.ReservationId);

                if (reservation == null)
                    throw new BusinessException("Reservation not found");

                // 2. Validar estado
                if (reservation.Status != ReservationStatus.Pending)
                    throw new BusinessException("Invalid reservation state");

                // 3. Validar expiración
                if (reservation.ExpiresAt < DateTime.UtcNow)
                {
                    reservation.Status = ReservationStatus.Expired;
                    await _reservationRepository.UpdateAsync(reservation);

                    throw new BusinessException("Reservation expired");
                }

                // 4. Confirmar reserva
                reservation.Status = ReservationStatus.Paid;

                // 5. Actualizar asiento
                var seat = await _seatRepository.GetByIdAsync(reservation.SeatId);

                if (seat == null)
                    throw new BusinessException("Seat not found");

                seat.Status = SeatStatus.Sold;

                // 6. Persistir cambios
                await _reservationRepository.UpdateAsync(reservation);
                await _seatRepository.UpdateAsync(seat);

                // 7. Auditoría éxito
                await _auditLogRepository.AddAsync(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = reservation.UserId,
                    Action = "CONFIRM_SUCCESS",
                    EntityType = "Reservation",
                    EntityId = reservation.Id.ToString(),
                    Details = $"Reservation confirmed for seat {seat.Id}",
                    CreatedAt = DateTime.UtcNow
                });

                return new ConfirmSeatResponse
                {
                    ReservationId = reservation.Id,
                    SeatId = seat.Id,
                    Status = reservation.Status.ToString(),
                    ConfirmedAt = DateTime.UtcNow
                };



            }
            catch (Exception ex)
            {
                // 8. Auditoría fallo
                await _auditLogRepository.AddAsync(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = command.UserId,
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

