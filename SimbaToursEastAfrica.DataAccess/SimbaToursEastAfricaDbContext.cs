using Microsoft.EntityFrameworkCore;
using SimbaToursEastAfrica.Domain.Models;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimbaToursEastAfrica.DataAccess
{
    public class SimbaToursEastAfricaDbContext:DbContext
    {

        public SimbaToursEastAfricaDbContext(DbContextOptions<SimbaToursEastAfricaDbContext> dbContextOptions) :base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseIdentityColumns();
            modelBuilder.Entity<Schedule>().HasOne<Driver>().WithMany(e=> e.Schedules).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Schedule>().HasOne<Itinary>().WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Schedule>().HasOne<Location>().WithMany().OnDelete(DeleteBehavior.Restrict);
           /* modelBuilder.Entity<Meal>().HasMany<Item>().WithOne().HasForeignKey(p => p.MealId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Item>().HasOne<Meal>().WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Laguage>().HasMany<Item>().WithOne().HasForeignKey(p => p.LaguageId).IsRequired(false).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Item>().HasOne<Laguage>().WithMany().OnDelete(DeleteBehavior.Restrict);*/
            modelBuilder.Entity<Laguage>().HasOne<TourClient>().WithMany().OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Destination> Destinations{ get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<HotelBooking> HotelBookings { get; set; }
        public DbSet<InAndOutBoundAirTravel> InAndOutBoundAirTravels { get; set; }
        public DbSet<InternalVehicleTravel> InternalVehicleTravels { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Itinary> Itinaries { get; set; }
        public DbSet<Laguage> Laguages { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<TourClient> TourClients { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<HotelPricing> HotelPricings { get; set; }
        public DbSet<LaguagePricing> LaguagePricings { get; set; }
        public DbSet<SchedulesPricing> SchedulesPricings { get; set; }
        public DbSet<MealPricing> MealPricings { get; set; }
        public DbSet<DealsPricing> DealsPricings { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<TransportPricing> TransportPricings { get; set; }
    }
}
