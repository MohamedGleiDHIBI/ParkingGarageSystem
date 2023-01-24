using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParkingGarageSystem.Models;

namespace ParkingGarageSystem.Infrastructure
{
    public class ParkingSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Garage> Garages { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public ParkingSystemDbContext(DbContextOptions<ParkingSystemDbContext> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Histories)
                .WithOne(h => h.User)
                .HasForeignKey(h => h.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Vehicle)
                .WithOne(v => v.User)
                .HasForeignKey<Vehicle>(v => v.UserId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Payment)
                .WithOne(p => p.Reservation)
                .HasForeignKey<Payment>(p => p.ReservationId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Location)
                .WithMany(l => l.Reservations)
                .HasForeignKey(r => r.LocationId);

            modelBuilder.Entity<Location>()
                .HasOne(l => l.Garage)
                .WithMany(g => g.Locations)
                .HasForeignKey(l => l.GarageId);

            modelBuilder.Entity<Garage>()
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Garage>()
                .Property(g => g.TotalSpots)
                .IsRequired();
            modelBuilder.Entity<Garage>()
                .Property(g => g.AvailableSpots)
                .IsRequired();
            modelBuilder.Entity<Garage>()
                .Property(g => g.Address)
                .IsRequired()
                .HasMaxLength(150);
            modelBuilder.Entity<Garage>()
                .Property(g => g.OperatingHours)
                .HasMaxLength(150);

            modelBuilder.Entity<History>()
                .HasOne(h => h.User)
                .WithMany(u => u.Histories)
                .HasForeignKey(h => h.UserId);

            modelBuilder.Entity<Location>()
                .Property(l => l.Name)
                .IsRequired()
                .HasMaxLength(100);
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.User)
                .WithOne(u => u.Vehicle)
                .HasForeignKey<Vehicle>(v => v.UserId);
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Reservation)
                .WithOne(r => r.Payment)
                .HasForeignKey<Payment>(p => p.ReservationId);
        }
    }
    /*
     *      A User can have multiple Reservations, and a Reservation belongs to a
     *      A User can have multiple Histories, and a History belongs to a User
     *      A User can have one Vehicle, and a Vehicle belongs to a User
     *      A Reservation belongs to a User and a Location, and a Location can have multiple Reservations
     *      A Reservation can have one Payment, and a Payment belongs to a Reservation
     *      A Location belongs to a Garage and a Garage can have multiple Locations
     *      A Payment belongs to a Reservation and a User, and a User can have multiple Payments
     *      A Garage has a name, total spots, available spots, address and operating hours
     *      A History belongs to a User and a Location, and a Location can have multiple Histories
     *      A Location has a name and a IsAvailable
     *      A Vehicle belongs to a User
    */
}
