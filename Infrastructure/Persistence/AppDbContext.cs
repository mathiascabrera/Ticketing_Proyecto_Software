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

namespace Infrastructure.Persistence
{
    public class AppDbContext:DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<AuditLog> Audits { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("EVENT");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                         .HasColumnType("int")
                         .ValueGeneratedOnAdd();

                entity.Property(c => c.EventDate)
                    .IsRequired();

                entity.Property(e => e.Venue)
                    .IsRequired();

                entity.Property(e => e.Status)
                    .IsRequired();

                entity.HasMany(e => e.SectorsList)
                .WithOne(s => s.EventObj)
                .HasForeignKey(e => e.EventId)
                .IsRequired();

                entity.HasData
                (new Event
                {
                    Id = 1,
                    Name = "Movie",
                    EventDate = new DateTime(2026, 07, 28),
                    Venue = "The Hobbit",
                    Status = "Available"
                });
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.ToTable("SECTOR");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

                entity.HasOne(e => e.EventObj)
                .WithMany(e => e.SectorsList)
                .HasForeignKey(e => e.EventId)
                .IsRequired();

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(30);

                entity.Property(e => e.Price)
                .HasColumnType("decimal(18,2)")
                .HasPrecision(18, 2)
                .IsRequired()
                .HasDefaultValue(0.00m);

                entity.Property(e => e.Capacity)
                    .HasColumnType("int")
                    .IsRequired();

                entity.HasMany(e => e.SeatsList)
                .WithOne(s => s.SectorObj)
                .HasForeignKey(e => e.SectorId)
                .IsRequired(false);

                entity.HasData
                (new Sector
                {
                    Id = 1,
                    EventId = 1,
                    Name = "VIP",
                    Price = 800,
                    Capacity = 100
                },
                new Sector
                {
                    Id = 2,
                    EventId = 1,
                    Name = "NORMAL",
                    Price = 450,
                    Capacity = 1000
                });
            });
            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("SEAT");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                .HasColumnType("uniqueidentifier")
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
                .HasConversion<string>()
                .HasMaxLength(20)
                .IsRequired();

                entity.Property(e => e.Version)
                .HasColumnType("int")
                .IsRequired();

                entity.HasOne(e => e.ReservationObj)
                .WithOne(r => r.SeatObj)
                .HasForeignKey<Reservation>()
                .IsRequired(false);

                entity.HasData
                (new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "A",
                    SeatNumber = 1,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "A",
                    SeatNumber = 2,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "A",
                    SeatNumber = 3,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "A",
                    SeatNumber = 4,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "A",
                    SeatNumber = 5,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "A",
                    SeatNumber = 6,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "A",
                    SeatNumber = 7,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "A",
                    SeatNumber = 8,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "A",
                    SeatNumber = 9,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "A",
                    SeatNumber = 10,
                    Status = (SeatStatus)0,
                    Version = 0
                },



                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "B",
                    SeatNumber = 1,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "B",
                    SeatNumber = 2,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "B",
                    SeatNumber = 3,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "B",
                    SeatNumber = 4,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "B",
                    SeatNumber = 5,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "B",
                    SeatNumber = 6,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "B",
                    SeatNumber = 7,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "B",
                    SeatNumber = 8,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "B",
                    SeatNumber = 9,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "B",
                    SeatNumber = 10,
                    Status = (SeatStatus)0,
                    Version = 0
                },





                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "C",
                    SeatNumber = 1,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "C",
                    SeatNumber = 2,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "C",
                    SeatNumber = 3,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "C",
                    SeatNumber = 4,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "C",
                    SeatNumber = 5,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "C",
                    SeatNumber = 6,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "C",
                    SeatNumber = 7,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "C",
                    SeatNumber = 8,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "C",
                    SeatNumber = 9,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "C",
                    SeatNumber = 10,
                    Status = (SeatStatus)0,
                    Version = 0
                },



                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "D",
                    SeatNumber = 1,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "D",
                    SeatNumber = 2,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "D",
                    SeatNumber = 3,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "D",
                    SeatNumber = 4,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "D",
                    SeatNumber = 5,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "D",
                    SeatNumber = 6,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "D",
                    SeatNumber = 7,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "D",
                    SeatNumber = 8,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "D",
                    SeatNumber = 9,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "D",
                    SeatNumber = 10,
                    Status = (SeatStatus)0,
                    Version = 0
                },



                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "E",
                    SeatNumber = 1,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "E",
                    SeatNumber = 2,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "E",
                    SeatNumber = 3,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "E",
                    SeatNumber = 4,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "E",
                    SeatNumber = 5,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "E",
                    SeatNumber = 6,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "E",
                    SeatNumber = 7,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "E",
                    SeatNumber = 8,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "E",
                    SeatNumber = 9,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 1,
                    RowIdentifier = "E",
                    SeatNumber = 10,
                    Status = (SeatStatus)0,
                    Version = 0
                },




                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "A",
                    SeatNumber = 1,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "A",
                    SeatNumber = 2,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "A",
                    SeatNumber = 3,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "A",
                    SeatNumber = 4,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "A",
                    SeatNumber = 5,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "A",
                    SeatNumber = 6,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "A",
                    SeatNumber = 7,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "A",
                    SeatNumber = 8,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "A",
                    SeatNumber = 9,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "A",
                    SeatNumber = 10,
                    Status = (SeatStatus)0,
                    Version = 0
                },



                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "B",
                    SeatNumber = 1,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "B",
                    SeatNumber = 2,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "B",
                    SeatNumber = 3,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "B",
                    SeatNumber = 4,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "B",
                    SeatNumber = 5,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "B",
                    SeatNumber = 6,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "B",
                    SeatNumber = 7,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "B",
                    SeatNumber = 8,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "B",
                    SeatNumber = 9,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "B",
                    SeatNumber = 10,
                    Status = (SeatStatus)0,
                    Version = 0
                },





                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "C",
                    SeatNumber = 1,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "C",
                    SeatNumber = 2,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "C",
                    SeatNumber = 3,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "C",
                    SeatNumber = 4,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "C",
                    SeatNumber = 5,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "C",
                    SeatNumber = 6,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "C",
                    SeatNumber = 7,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "C",
                    SeatNumber = 8,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "C",
                    SeatNumber = 9,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "C",
                    SeatNumber = 10,
                    Status = (SeatStatus)0,
                    Version = 0
                },



                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "D",
                    SeatNumber = 1,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "D",
                    SeatNumber = 2,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "D",
                    SeatNumber = 3,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "D",
                    SeatNumber = 4,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "D",
                    SeatNumber = 5,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "D",
                    SeatNumber = 6,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "D",
                    SeatNumber = 7,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "D",
                    SeatNumber = 8,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "D",
                    SeatNumber = 9,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "D",
                    SeatNumber = 10,
                    Status = (SeatStatus)0,
                    Version = 0
                },



                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "E",
                    SeatNumber = 1,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "E",
                    SeatNumber = 2,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "E",
                    SeatNumber = 3,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "E",
                    SeatNumber = 4,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "E",
                    SeatNumber = 5,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "E",
                    SeatNumber = 6,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "E",
                    SeatNumber = 7,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "E",
                    SeatNumber = 8,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "E",
                    SeatNumber = 9,
                    Status = (SeatStatus)0,
                    Version = 0
                },
                new Seat
                {
                    SectorId = 2,
                    RowIdentifier = "E",
                    SeatNumber = 10,
                    Status = (SeatStatus)0,
                    Version = 0
                }





                );
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

                entity.HasOne(e => e.UserObj)
                .WithMany(u => u.ReservationList)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("USER");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                .HasColumnType("int")
                .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(12);

                entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(254);

                entity.Property(e => e.PasswordHash)
                .IsRequired();

                entity.HasMany(e => e.ReservationList)
                .WithOne(r => r.UserObj)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false);

                entity.HasMany(e => e.AuditLogList)
                .WithOne(a => a.UserObj)
                .HasForeignKey(e => e.UserId)
                .IsRequired(false);
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
