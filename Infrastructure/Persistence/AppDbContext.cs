using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
            warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("EVENT");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.EventDate)
                    .IsRequired();

                entity.Property(e => e.Venue)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Status)
                    .IsRequired();

                entity.Property(e => e.Description)
                    .HasMaxLength(500);

                entity.Property(e => e.Url1)
                    .HasMaxLength(255);

                entity.Property(e => e.Url2)
                    .HasMaxLength(255);

            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.ToTable("SECTOR");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18,2)")
                    .IsRequired()
                    .HasDefaultValue(0.00m);

                entity.Property(e => e.Capacity)
                    .HasColumnType("int")
                    .IsRequired();

                //  layout del sector (lo que mandaba el front)
                entity.Property(e => e.Rows)
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(e => e.Cols)
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(e => e.GridX)
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(e => e.GridY)
                    .HasColumnType("int")
                    .IsRequired();

                entity.HasOne(e => e.EventObj)
                    .WithMany(e => e.SectorsList)
                    .HasForeignKey(e => e.EventId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.SeatsList)
                    .WithOne(s => s.SectorObj)
                    .HasForeignKey(s => s.SectorId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                //  regla importante: nombre único por evento
                entity.HasIndex(e => new { e.EventId, e.Name })
                    .IsUnique();

            });
            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("SEAT");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();

                entity.HasOne(e => e.SectorObj)
                    .WithMany(s => s.SeatsList)
                    .HasForeignKey(e => e.SectorId)
                    .IsRequired(false);

                entity.Property(e => e.RowIdentifier)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.SeatNumber)
                    .HasColumnType("int")
                    .IsRequired();

                entity.Property(e => e.Status)
                    .HasConversion<int>()
                    .HasColumnType("int")
                    .IsRequired();

          
                entity.Property(e => e.RowVersion)
                    .IsRowVersion();

                entity.HasMany(e => e.ReservationSeats)
                    .WithOne(rs => rs.SeatObj)
                    .HasForeignKey(rs => rs.SeatId);

            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("RESERVATION");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("uniqueidentifier")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.ReservedAt)
                    .IsRequired();

                entity.Property(e => e.ExpiresAt)
                    .IsRequired();

                // relación con User
                entity.HasOne(e => e.UserObj)
                    .WithMany(u => u.ReservationList)
                    .HasForeignKey(e => e.UserId)
                    .IsRequired();

                // relación con ReservationSeat
                entity.HasMany(e => e.Seats)
                    .WithOne(rs => rs.ReservationObj)
                    .HasForeignKey(rs => rs.ReservationId);

            });

            modelBuilder.Entity<ReservationSeat>(entity =>
            {
                entity.ToTable("RESERVATION_SEAT");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("uniqueidentifier")
                    .ValueGeneratedOnAdd();

                // relación con Reservation
                entity.HasOne(e => e.ReservationObj)
                    .WithMany(r => r.Seats)
                    .HasForeignKey(e => e.ReservationId);

                // relación con Seat
                entity.HasOne(e => e.SeatObj)
                    .WithMany(s => s.ReservationSeats)
                    .HasForeignKey(e => e.SeatId);

                // evitar duplicados
                entity.HasIndex(e => new { e.ReservationId, e.SeatId })
                    .IsUnique();
            });


            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AUDIT_LOG");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedOnAdd();

                entity.Property(e => e.Action)
                .IsRequired();

                entity.Property(e => e.EntityType)
                .IsRequired();

                entity.Property(e => e.EntityId)
                .IsRequired();

                entity.Property(e => e.Details)
                .IsRequired();

                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                entity.Property(e => e.UserId)
                .IsRequired(false);

                entity.HasOne(e => e.UserObj)
                .WithMany(u => u.AuditLogList)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false);
            });

        }
    }
}
