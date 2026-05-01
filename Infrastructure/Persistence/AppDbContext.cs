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

namespace Infrastructure.Persistence
{
    public class AppDbContext:DbContext
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }
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

            // =========================
            // EVENT
            // =========================
            modelBuilder.Entity<Event>(entity =>
            {
                entity.ToTable("EVENT");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.EventDate).IsRequired();
                entity.Property(e => e.Venue).IsRequired();
                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.ImageUrl).HasMaxLength(500).IsRequired(false);

                entity.HasMany(e => e.SectorsList)
                    .WithOne(s => s.EventObj)
                    .HasForeignKey(s => s.EventId)
                    .IsRequired();

                entity.HasData(
                    new Event
                    {
                        Id = 1,
                        Name = "concierto de rock",
                        EventDate = new DateTime(2026, 7, 28),
                        Venue = "la renga",
                        Status = "Available"
                    }
                );
            });

            // =========================
            // SECTOR
            // =========================
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
                    .IsRequired();

                entity.Property(e => e.Capacity)
                    .IsRequired();

                entity.HasMany(e => e.SeatsList)
                    .WithOne(s => s.SectorObj)
                    .HasForeignKey(e => e.SectorId)
                    .IsRequired(false);

                entity.HasData(
                    new Sector { Id = 1, EventId = 1, Name = "VIP", Price = 800, Capacity = 100 },
                    new Sector { Id = 2, EventId = 1, Name = "NORMAL", Price = 450, Capacity = 1000 }
                );
            });

            // =========================
            // SEAT
            // =========================
            modelBuilder.Entity<Seat>(entity =>
            {
                entity.ToTable("SEAT");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("uniqueidentifier");

                entity.HasOne(e => e.SectorObj)
                    .WithMany(s => s.SeatsList)
                    .HasForeignKey(e => e.SectorId)
                    .IsRequired(false);

                entity.Property(e => e.RowIdentifier)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.SeatNumber)
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
                    .HasForeignKey<Reservation>(r => r.SeatId)
                    .IsRequired(false);

                entity.HasData(
// Sector 1
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111001"), SectorId = 1, RowIdentifier = "A", SeatNumber = 1, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111002"), SectorId = 1, RowIdentifier = "A", SeatNumber = 2, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111003"), SectorId = 1, RowIdentifier = "A", SeatNumber = 3, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111004"), SectorId = 1, RowIdentifier = "A", SeatNumber = 4, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111005"), SectorId = 1, RowIdentifier = "A", SeatNumber = 5, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111006"), SectorId = 1, RowIdentifier = "A", SeatNumber = 6, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111007"), SectorId = 1, RowIdentifier = "A", SeatNumber = 7, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111008"), SectorId = 1, RowIdentifier = "A", SeatNumber = 8, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111009"), SectorId = 1, RowIdentifier = "A", SeatNumber = 9, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111010"), SectorId = 1, RowIdentifier = "A", SeatNumber = 10, Status = SeatStatus.Available, Version = 0 },

                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111011"), SectorId = 1, RowIdentifier = "B", SeatNumber = 1, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111012"), SectorId = 1, RowIdentifier = "B", SeatNumber = 2, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111013"), SectorId = 1, RowIdentifier = "B", SeatNumber = 3, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111014"), SectorId = 1, RowIdentifier = "B", SeatNumber = 4, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111015"), SectorId = 1, RowIdentifier = "B", SeatNumber = 5, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111016"), SectorId = 1, RowIdentifier = "B", SeatNumber = 6, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111017"), SectorId = 1, RowIdentifier = "B", SeatNumber = 7, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111018"), SectorId = 1, RowIdentifier = "B", SeatNumber = 8, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111019"), SectorId = 1, RowIdentifier = "B", SeatNumber = 9, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111020"), SectorId = 1, RowIdentifier = "B", SeatNumber = 10, Status = SeatStatus.Available, Version = 0 },

                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111021"), SectorId = 1, RowIdentifier = "C", SeatNumber = 1, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111022"), SectorId = 1, RowIdentifier = "C", SeatNumber = 2, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111023"), SectorId = 1, RowIdentifier = "C", SeatNumber = 3, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111024"), SectorId = 1, RowIdentifier = "C", SeatNumber = 4, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111025"), SectorId = 1, RowIdentifier = "C", SeatNumber = 5, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111026"), SectorId = 1, RowIdentifier = "C", SeatNumber = 6, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111027"), SectorId = 1, RowIdentifier = "C", SeatNumber = 7, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111028"), SectorId = 1, RowIdentifier = "C", SeatNumber = 8, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111029"), SectorId = 1, RowIdentifier = "C", SeatNumber = 9, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111030"), SectorId = 1, RowIdentifier = "C", SeatNumber = 10, Status = SeatStatus.Available, Version = 0 },

                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111031"), SectorId = 1, RowIdentifier = "D", SeatNumber = 1, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111032"), SectorId = 1, RowIdentifier = "D", SeatNumber = 2, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111033"), SectorId = 1, RowIdentifier = "D", SeatNumber = 3, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111034"), SectorId = 1, RowIdentifier = "D", SeatNumber = 4, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111035"), SectorId = 1, RowIdentifier = "D", SeatNumber = 5, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111036"), SectorId = 1, RowIdentifier = "D", SeatNumber = 6, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111037"), SectorId = 1, RowIdentifier = "D", SeatNumber = 7, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111038"), SectorId = 1, RowIdentifier = "D", SeatNumber = 8, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111039"), SectorId = 1, RowIdentifier = "D", SeatNumber = 9, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111040"), SectorId = 1, RowIdentifier = "D", SeatNumber = 10, Status = SeatStatus.Available, Version = 0 },

                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111041"), SectorId = 1, RowIdentifier = "E", SeatNumber = 1, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111042"), SectorId = 1, RowIdentifier = "E", SeatNumber = 2, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111043"), SectorId = 1, RowIdentifier = "E", SeatNumber = 3, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111044"), SectorId = 1, RowIdentifier = "E", SeatNumber = 4, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111045"), SectorId = 1, RowIdentifier = "E", SeatNumber = 5, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111046"), SectorId = 1, RowIdentifier = "E", SeatNumber = 6, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111047"), SectorId = 1, RowIdentifier = "E", SeatNumber = 7, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111048"), SectorId = 1, RowIdentifier = "E", SeatNumber = 8, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111049"), SectorId = 1, RowIdentifier = "E", SeatNumber = 9, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("11111111-1111-1111-1111-111111111050"), SectorId = 1, RowIdentifier = "E", SeatNumber = 10, Status = SeatStatus.Available, Version = 0 },

                    // Sector 2
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222001"), SectorId = 2, RowIdentifier = "A", SeatNumber = 1, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222002"), SectorId = 2, RowIdentifier = "A", SeatNumber = 2, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222003"), SectorId = 2, RowIdentifier = "A", SeatNumber = 3, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222004"), SectorId = 2, RowIdentifier = "A", SeatNumber = 4, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222005"), SectorId = 2, RowIdentifier = "A", SeatNumber = 5, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222006"), SectorId = 2, RowIdentifier = "A", SeatNumber = 6, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222007"), SectorId = 2, RowIdentifier = "A", SeatNumber = 7, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222008"), SectorId = 2, RowIdentifier = "A", SeatNumber = 8, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222009"), SectorId = 2, RowIdentifier = "A", SeatNumber = 9, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222010"), SectorId = 2, RowIdentifier = "A", SeatNumber = 10, Status = SeatStatus.Available, Version = 0 },

                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222011"), SectorId = 2, RowIdentifier = "B", SeatNumber = 1, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222012"), SectorId = 2, RowIdentifier = "B", SeatNumber = 2, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222013"), SectorId = 2, RowIdentifier = "B", SeatNumber = 3, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222014"), SectorId = 2, RowIdentifier = "B", SeatNumber = 4, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222015"), SectorId = 2, RowIdentifier = "B", SeatNumber = 5, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222016"), SectorId = 2, RowIdentifier = "B", SeatNumber = 6, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222017"), SectorId = 2, RowIdentifier = "B", SeatNumber = 7, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222018"), SectorId = 2, RowIdentifier = "B", SeatNumber = 8, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222019"), SectorId = 2, RowIdentifier = "B", SeatNumber = 9, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222020"), SectorId = 2, RowIdentifier = "B", SeatNumber = 10, Status = SeatStatus.Available, Version = 0 },

                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222021"), SectorId = 2, RowIdentifier = "C", SeatNumber = 1, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222022"), SectorId = 2, RowIdentifier = "C", SeatNumber = 2, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222023"), SectorId = 2, RowIdentifier = "C", SeatNumber = 3, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222024"), SectorId = 2, RowIdentifier = "C", SeatNumber = 4, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222025"), SectorId = 2, RowIdentifier = "C", SeatNumber = 5, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222026"), SectorId = 2, RowIdentifier = "C", SeatNumber = 6, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222027"), SectorId = 2, RowIdentifier = "C", SeatNumber = 7, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222028"), SectorId = 2, RowIdentifier = "C", SeatNumber = 8, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222029"), SectorId = 2, RowIdentifier = "C", SeatNumber = 9, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222030"), SectorId = 2, RowIdentifier = "C", SeatNumber = 10, Status = SeatStatus.Available, Version = 0 },

                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222031"), SectorId = 2, RowIdentifier = "D", SeatNumber = 1, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222032"), SectorId = 2, RowIdentifier = "D", SeatNumber = 2, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222033"), SectorId = 2, RowIdentifier = "D", SeatNumber = 3, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222034"), SectorId = 2, RowIdentifier = "D", SeatNumber = 4, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222035"), SectorId = 2, RowIdentifier = "D", SeatNumber = 5, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222036"), SectorId = 2, RowIdentifier = "D", SeatNumber = 6, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222037"), SectorId = 2, RowIdentifier = "D", SeatNumber = 7, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222038"), SectorId = 2, RowIdentifier = "D", SeatNumber = 8, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222039"), SectorId = 2, RowIdentifier = "D", SeatNumber = 9, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222040"), SectorId = 2, RowIdentifier = "D", SeatNumber = 10, Status = SeatStatus.Available, Version = 0 },

                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222041"), SectorId = 2, RowIdentifier = "E", SeatNumber = 1, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222042"), SectorId = 2, RowIdentifier = "E", SeatNumber = 2, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222043"), SectorId = 2, RowIdentifier = "E", SeatNumber = 3, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222044"), SectorId = 2, RowIdentifier = "E", SeatNumber = 4, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222045"), SectorId = 2, RowIdentifier = "E", SeatNumber = 5, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222046"), SectorId = 2, RowIdentifier = "E", SeatNumber = 6, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222047"), SectorId = 2, RowIdentifier = "E", SeatNumber = 7, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222048"), SectorId = 2, RowIdentifier = "E", SeatNumber = 8, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222049"), SectorId = 2, RowIdentifier = "E", SeatNumber = 9, Status = SeatStatus.Available, Version = 0 },
                    new Seat { Id = Guid.Parse("22222222-2222-2222-2222-222222222050"), SectorId = 2, RowIdentifier = "E", SeatNumber = 10, Status = SeatStatus.Available, Version = 0 }
                );
            });


            // RESERVATION

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("RESERVATION");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.Status)
                    .HasConversion<string>()
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.ReservedAt).IsRequired();
                entity.Property(e => e.ExpiresAt).IsRequired();

                entity.HasOne(e => e.UserObj)
                    .WithMany(u => u.ReservationList)
                    .HasForeignKey(e => e.UserId)
                    .IsRequired();
            });

 
            // USER

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
                    .HasForeignKey(r => r.UserId)
                    .IsRequired(false);

                entity.HasMany(e => e.AuditLogList)
                    .WithOne(a => a.UserObj)
                    .HasForeignKey(a => a.UserId)
                    .IsRequired(false);

                entity.HasData(
                    new User
                    {
                        Id = 1,
                        Name = "Test User",
                        Email = "test@test.com",
                        PasswordHash = "1234"
                    }
                );


            });


            // AUDIT LOG

            modelBuilder.Entity<AuditLog>(entity =>
            {
                entity.ToTable("AUDIT_LOG");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id)
                    .HasColumnType("uniqueidentifier");

                entity.Property(e => e.Action).IsRequired();
                entity.Property(e => e.EntityType).IsRequired();
                entity.Property(e => e.EntityId).IsRequired();
                entity.Property(e => e.Details).IsRequired();
                entity.Property(e => e.CreatedAt).IsRequired();

                entity.Property(e => e.UserId).IsRequired(false);

                entity.HasOne(e => e.UserObj)
                    .WithMany(u => u.AuditLogList)
                    .HasForeignKey(e => e.UserId)
                    .IsRequired(false);
            });
        }
    }
}
