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
using Microsoft.EntityFrameworkCore;

namespace Application.UsesCases.Reservations.Handlers
{
    public class ReserveSeatsHandler : IReserveSeatsHandler
    {
        private readonly ISeatRepository _seatRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IUnitOfWork _uow;

        public ReserveSeatsHandler(
            ISeatRepository seatRepository,
            IReservationRepository reservationRepository,
            IAuditLogRepository auditLogRepository,
            IUnitOfWork uow)
        {
            _seatRepository = seatRepository;
            _reservationRepository = reservationRepository;
            _auditLogRepository = auditLogRepository;
            _uow = uow;
        }

        public async Task<ReserveSeatsResponse> Handle(ReserveSeatsCommand command, string userId)
        {
            await _uow.BeginTransactionAsync();  // ---- ROLLBACK ---- comienza la transanccion 

            try
            {
                // 1. Traer asientos
                var seats = await _seatRepository.GetByIdsAsync(command.SeatsIds);

                if (seats.Count != command.SeatsIds.Count)
                    throw new Exception("Algunos asientos no existen");

                // 2. Validar disponibilidad REAL
                if (seats.Any(s => s.Status != SeatStatus.Available))
                    throw new Exception("Uno o más asientos ya están reservados");

                // 3. Crear reserva
                var reservation = new Reservation
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Status = ReservationStatus.Pending,
                    ReservedAt = DateTime.UtcNow,
                    ExpiresAt = DateTime.UtcNow.AddMinutes(3),
                    Seats = new List<ReservationSeat>()
                };

                // 4. Marcar asientos como reservados + vincular
                foreach (var seat in seats)
                {
                    seat.Status = SeatStatus.Reserved;

                    reservation.Seats.Add(new ReservationSeat
                    {
                        Id = Guid.NewGuid(),
                        SeatId = seat.Id,
                        ReservationId = reservation.Id
                    });
                }

                // 5. Guardar reserva
                await _reservationRepository.AddAsync(reservation);

                // 6. Guardar cambios (incluye Seats con RowVersion)
                await _uow.SaveChangesAsync();

                await _uow.CommitAsync();  //---- ROLLBACK ---- se confirma la transaccion y los cambios quedan permanentes 

                // 7. Auditoría (no rompe flujo)
                await SafeAudit(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Action = "ReserveSeats",
                    EntityType = "Reservation",
                    EntityId = reservation.Id.ToString(),
                    Details = $"Seats: {string.Join(",", command.SeatsIds)}",
                    CreatedAt = DateTime.UtcNow
                });
                
                return new ReserveSeatsResponse
                {
                    ReservationId = reservation.Id,
                    Message = "Reserva exitosa"
                };
            }
            catch (DbUpdateConcurrencyException)
            {
                await _uow.RollbackAsync();  //---- ROLLBACK ---- se desehacen todos lo cambios hechos 

                await SafeAudit(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Action = "ReserveSeats",
                    EntityType = "Reservation",
                    Details = "Conflicto de concurrencia (RowVersion)",
                    CreatedAt = DateTime.UtcNow
                });

                throw new BusinessException("One or more seats are not available");
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();

                await SafeAudit(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Action = "ReserveSeats",
                    EntityType = "Reservation",
                    EntityId = string.Join(",", command.SeatsIds),
                    Details = $"Error: {ex.Message}",
                    CreatedAt = DateTime.UtcNow
                });

                throw;
            }
        }

        private async Task SafeAudit(AuditLog log)
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
