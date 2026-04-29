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
    public class ReserveSeatHandler : IReserveSeatHandler
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IAuditLogRepository _auditLogRepository;

        public ReserveSeatHandler(
            ISeatRepository seatRepository,
            IReservationRepository reservationRepository,
            IAuditLogRepository auditLogRepository)
        {
            _seatRepository = seatRepository;
            _reservationRepository = reservationRepository;
            _auditLogRepository = auditLogRepository;
        }

        public async Task<ReserveSeatResponse> Handle(ReserveSeatCommand command)
        {
            try
            {
                var seat = await _seatRepository.GetByIdAsync(command.SeatId);

                if (seat == null)
                    throw new BusinessException("Seat not found");

                if (seat.Status != SeatStatus.Available)
                    throw new BusinessException("Seat is not available");

                var hasReservation = await _reservationRepository.HasActiveReservation(command.SeatId);

                if (hasReservation)
                    throw new BusinessException("Seat already reserved");

                var reservation = new Reservation
                {
                    Id = Guid.NewGuid(),
                    SeatId = command.SeatId,
                    UserId = command.UserId,
                    ReservedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(5),
                    Status = ReservationStatus.Pending
                };

              
                seat.Status = SeatStatus.Reserved;

                await _seatRepository.UpdateAsync(seat);
                await _reservationRepository.AddAsync(reservation);

                // ✔ auditoría éxito
                await _auditLogRepository.AddAsync(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = command.UserId,
                    Action = "RESERVE_SUCCESS",
                    EntityType = "Reservation",
                    EntityId = reservation.Id.ToString(),
                    Details = $"Seat {seat.Id} reserved",
                    CreatedAt = DateTime.UtcNow
                });

                return new ReserveSeatResponse
                {
                    ReservationId = reservation.Id,
                    ExpiresAt = reservation.ExpiresAt
                };


            }
            catch (ConcurrencyException)
            {
                // ❌ conflicto real de concurrencia (RowVersion)
                await _auditLogRepository.AddAsync(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = command.UserId,
                    Action = "RESERVE_CONCURRENCY_FAIL",
                    EntityType = "Seat",
                    EntityId = command.SeatId.ToString(),
                    Details = "Seat was modified by another user",
                    CreatedAt = DateTime.UtcNow
                });

                throw new BusinessException("Seat already reserved by another user");
            }
            catch (Exception ex)
            {
                // ❌ error general
                await _auditLogRepository.AddAsync(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = command.UserId,
                    Action = "RESERVE_FAILED",
                    EntityType = "Seat",
                    EntityId = command.SeatId.ToString(),
                    Details = ex.Message,
                    CreatedAt = DateTime.UtcNow
                });

                throw;
            }
        }
    }
}
