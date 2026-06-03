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
        private readonly IUnitOfWork _uow;
        public ConfirmSeatHandler(
            IReservationRepository reservationRepository,
            ISeatRepository seatRepository,
            IAuditLogRepository auditLogRepository,
            IUnitOfWork uow)
        {
            _reservationRepository = reservationRepository;
            _seatRepository = seatRepository;
            _auditLogRepository = auditLogRepository;
            _uow = uow;
        }

        public async Task<ConfirmSeatResponse> Handle(ConfirmSeatCommand command)
        {
            string userId = "";

            try
            {
                var reservation = await _reservationRepository.GetByIdWithSeats(command.ReservationId);

                if (reservation == null)
                    throw new BusinessException("Reservation not found");

                userId = reservation.UserId;

                if (reservation.Status != ReservationStatus.Pending)
                    throw new BusinessException("Invalid reservation state");

                // Expirada (sin transacción iniciada innecesariamente )
                if (reservation.ExpiresAt < DateTime.UtcNow)
                {
                    reservation.Status = ReservationStatus.Expired;
                    await _uow.SaveChangesAsync();

                    throw new BusinessException("Reservation expired");
                }

                var seats = reservation.Seats
                    .Select(rs => rs.SeatObj)
                    .ToList();

                if (!seats.Any())
                    throw new BusinessException("No seats in reservation");

                await _uow.BeginTransactionAsync();

                try
                {
                    foreach (var seat in seats)
                        seat.Status = SeatStatus.Sold;

                    reservation.Status = ReservationStatus.Paid;

                    await _uow.SaveChangesAsync();
                    await _uow.CommitAsync();
                }
                catch
                {
                    await _uow.RollbackAsync();
                    throw;
                }

                // Auditoría guarda la tansaccion exitosa (fuera de la transacción de negocio)
                await SafeAudit(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Action = "CONFIRM_SUCCESS",
                    EntityType = "Reservation",
                    EntityId = reservation.Id.ToString(),
                    Details = $"Reservation confirmed for seats: {string.Join(",", seats.Select(s => s.Id))}",
                    CreatedAt = DateTime.UtcNow
                });

                return new ConfirmSeatResponse
                {
                    ReservationId = reservation.Id,
                    Status = reservation.Status.ToString(),
                    ConfirmedAt = DateTime.UtcNow
                };
            }
            catch (Exception ex)
            {
                // Auditoría de fallo (sin transacción de reserva)
                await SafeAudit(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Action = "CONFIRM_FAILED",
                    EntityType = "Reservation",
                    EntityId = command.ReservationId.ToString(),
                    Details = ex.Message,
                    CreatedAt = DateTime.UtcNow
                });

                throw;
            }
        }
        private async Task SafeAudit(AuditLog log) // auditoria fuera de de la transaccion por si falla no afecte a la operacion princiapl
        {
            try
            {
                await _auditLogRepository.AddAsync(log);
            }
            catch
            {
                // nunca romper flujo por auditoría
            }
        }
    }


}

