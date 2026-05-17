using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;

namespace Infrastructure.Services
{
    public class ReservationExpirationService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ReservationExpirationService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ProcessExpiredReservations(stoppingToken);

                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }

        private async Task ProcessExpiredReservations(CancellationToken ct)
        {
            using var scope = _scopeFactory.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var auditLogRepository =
                scope.ServiceProvider.GetRequiredService<IAuditLogRepository>();

            var now = DateTime.UtcNow;

            var expiredReservations = await db.Reservations
                .Include(r => r.Seats)
                    .ThenInclude(rs => rs.SeatObj)
                .Where(r =>
                    r.Status == ReservationStatus.Pending &&
                    r.ExpiresAt < now)
                .ToListAsync(ct);

            if (!expiredReservations.Any())
                return;

            foreach (var reservation in expiredReservations)
            {
                reservation.Status = ReservationStatus.Expired;

                foreach (var reservationSeat in reservation.Seats)
                {
                    reservationSeat.SeatObj.Status = SeatStatus.Available;
                }

                await auditLogRepository.AddAsync(new AuditLog
                {
                    Id = Guid.NewGuid(),
                    UserId = reservation.UserId,
                    Action = "RESERVATION_EXPIRED",
                    EntityType = "Reservation",
                    EntityId = reservation.Id.ToString(),
                    Details = $"Reservation expired automatically",
                    CreatedAt = DateTime.UtcNow
                });
            }

            await db.SaveChangesAsync(ct);
        }
    }
}
