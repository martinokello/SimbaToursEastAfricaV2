﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimbaToursEastAfrica.DataAccess;

namespace SimbaToursEastAfrica.Migrations
{
    [DbContext(typeof(SimbaToursEastAfricaDbContext))]
    [Migration("20180808104051_resourcePricing")]
    partial class resourcePricing
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("Country");

                    b.Property<string>("PhoneNumber");

                    b.Property<string>("PostCode");

                    b.Property<string>("Town");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.DealsPricing", b =>
                {
                    b.Property<int>("DealsPricingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DealName");

                    b.Property<string>("Description");

                    b.Property<decimal>("Price");

                    b.HasKey("DealsPricingId");

                    b.ToTable("DealsPricings");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Destination", b =>
                {
                    b.Property<int>("DestinationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ArriveTime");

                    b.Property<DateTime>("DepartTime");

                    b.Property<string>("DestinationFrom");

                    b.Property<string>("DestinationTo");

                    b.HasKey("DestinationId");

                    b.ToTable("Destinations");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Driver", b =>
                {
                    b.Property<int>("DriverId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<int>("VehicleId");

                    b.HasKey("DriverId");

                    b.HasIndex("VehicleId");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.HotelBooking", b =>
                {
                    b.Property<int>("HotelBookingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("AccomodationCost");

                    b.Property<bool>("HasMealsIncluded");

                    b.Property<string>("HotelName");

                    b.Property<int>("HotelPricingId");

                    b.Property<int>("LocationId");

                    b.Property<int>("TourClientId");

                    b.HasKey("HotelBookingId");

                    b.HasIndex("HotelPricingId");

                    b.HasIndex("LocationId");

                    b.HasIndex("TourClientId");

                    b.ToTable("HotelBookings");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.HotelPricing", b =>
                {
                    b.Property<int>("HotelPricingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.HasKey("HotelPricingId");

                    b.ToTable("HotelPricings");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.InAndOutBoundAirTravel", b =>
                {
                    b.Property<int>("InAndOutBoundAirTravelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DestinationId");

                    b.Property<decimal>("FlightCost");

                    b.Property<string>("FlightNumber");

                    b.Property<bool>("HasMealsIncluded");

                    b.Property<int>("LaguageId");

                    b.HasKey("InAndOutBoundAirTravelId");

                    b.HasIndex("DestinationId");

                    b.HasIndex("LaguageId");

                    b.ToTable("InAndOutBoundAirTravels");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.InternalVehicleTravel", b =>
                {
                    b.Property<int>("InternalVehicleTravelId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("VehicleCosts");

                    b.Property<int>("VehicleId");

                    b.HasKey("InternalVehicleTravelId");

                    b.HasIndex("VehicleId");

                    b.ToTable("InternalVehicleTravels");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Invoice", b =>
                {
                    b.Property<int>("InvoiceId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("GrossCost");

                    b.Property<string>("InvoiceName");

                    b.Property<decimal>("NetCost");

                    b.Property<decimal>("PercentTaxAppliable");

                    b.Property<int>("TourClientId");

                    b.HasKey("InvoiceId");

                    b.HasIndex("TourClientId");

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("InvoiceId");

                    b.Property<decimal>("ItemCost");

                    b.Property<int>("ItemType");

                    b.Property<int?>("LaguageId");

                    b.Property<int?>("MealId");

                    b.Property<int>("Quantity");

                    b.HasKey("ItemId");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("LaguageId");

                    b.HasIndex("MealId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Itinary", b =>
                {
                    b.Property<int>("ItinaryId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.HasKey("ItinaryId");

                    b.ToTable("Itinaries");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Laguage", b =>
                {
                    b.Property<int>("LaguageId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("LaguagePricingId");

                    b.Property<int>("TourClientId");

                    b.Property<int?>("TourClientId1");

                    b.HasKey("LaguageId");

                    b.HasIndex("LaguagePricingId");

                    b.HasIndex("TourClientId");

                    b.HasIndex("TourClientId1");

                    b.ToTable("Laguages");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.LaguagePricing", b =>
                {
                    b.Property<int>("LaguagePricingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LaguageDescription");

                    b.Property<string>("LaguagePricingName");

                    b.Property<decimal>("Price");

                    b.HasKey("LaguagePricingId");

                    b.ToTable("LaguagePricings");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AddressId");

                    b.Property<string>("Country");

                    b.HasKey("LocationId");

                    b.HasIndex("AddressId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Meal", b =>
                {
                    b.Property<int>("MealId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MealPricingId");

                    b.Property<int>("TourClientId");

                    b.HasKey("MealId");

                    b.HasIndex("MealPricingId");

                    b.HasIndex("TourClientId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.MealPricing", b =>
                {
                    b.Property<int>("MealPricingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("MealDescription");

                    b.Property<string>("MealName");

                    b.Property<decimal>("Price");

                    b.HasKey("MealPricingId");

                    b.ToTable("MealPricings");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Schedule", b =>
                {
                    b.Property<int>("ScheduleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DriverId");
                    

                    b.Property<int>("ItinaryId");

                    b.Property<int>("LocationId");

                    b.Property<string>("Operation");

                    b.Property<int>("SchedulesId");

                    b.Property<int>("SchedulesPricingId");

                    b.Property<DateTime>("TimeFrom");

                    b.Property<DateTime>("TimeTo");

                    b.HasKey("ScheduleId");

                    b.HasIndex("DriverId");
                    

                    b.HasIndex("ItinaryId");
                    

                    b.HasIndex("LocationId");
                    

                    b.HasIndex("SchedulesId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.SchedulesPricing", b =>
                {
                    b.Property<int>("SchedulesPricingId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Price");

                    b.Property<string>("SchedulesDescription");

                    b.HasKey("SchedulesPricingId");

                    b.ToTable("SchedulesPricings");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.TourClient", b =>
                {
                    b.Property<int>("TourClientId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClientFirstName");

                    b.Property<string>("ClientLastName");

                    b.Property<decimal>("GrossTotalCosts");

                    b.Property<bool>("HasRequiredVisaStatus");

                    b.Property<string>("Nationality");

                    b.Property<int>("NumberOfIndividuals");

                    b.HasKey("TourClientId");

                    b.ToTable("TourClients");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Vehicle", b =>
                {
                    b.Property<int>("VehicleId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AcutualNumberOfPassengersAllocated");

                    b.Property<int>("MaxNumberOfPassengers");

                    b.Property<int>("TourClientId");

                    b.Property<string>("VehicleRegistration");

                    b.Property<int>("VehicleType");

                    b.HasKey("VehicleId");

                    b.HasIndex("TourClientId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Driver", b =>
                {
                    b.HasOne("SimbaToursEastAfrica.Domain.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.HotelBooking", b =>
                {
                    b.HasOne("SimbaToursEastAfrica.Domain.Models.HotelPricing", "HotelPricing")
                        .WithMany()
                        .HasForeignKey("HotelPricingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimbaToursEastAfrica.Domain.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimbaToursEastAfrica.Domain.Models.TourClient", "TourClient")
                        .WithMany("HotelBookings")
                        .HasForeignKey("TourClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.InAndOutBoundAirTravel", b =>
                {
                    b.HasOne("SimbaToursEastAfrica.Domain.Models.Destination", "FromAndToDestination")
                        .WithMany()
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimbaToursEastAfrica.Domain.Models.Laguage", "CustomerLaguage")
                        .WithMany()
                        .HasForeignKey("LaguageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.InternalVehicleTravel", b =>
                {
                    b.HasOne("SimbaToursEastAfrica.Domain.Models.Vehicle", "VehicleAllocated")
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Invoice", b =>
                {
                    b.HasOne("SimbaToursEastAfrica.Domain.Models.TourClient", "TourClient")
                        .WithMany()
                        .HasForeignKey("TourClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Item", b =>
                {
                    b.HasOne("SimbaToursEastAfrica.Domain.Models.Invoice")
                        .WithMany("InvoicedItems")
                        .HasForeignKey("InvoiceId");

                    b.HasOne("SimbaToursEastAfrica.Domain.Models.Laguage", "Laguage")
                        .WithMany()
                        .HasForeignKey("LaguageId");

                    b.HasOne("SimbaToursEastAfrica.Domain.Models.Meal", "Meal")
                        .WithMany()
                        .HasForeignKey("MealId");
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Laguage", b =>
                {
                    b.HasOne("SimbaToursEastAfrica.Domain.Models.LaguagePricing", "LaguagePricing")
                        .WithMany()
                        .HasForeignKey("LaguagePricingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimbaToursEastAfrica.Domain.Models.TourClient", "TourClient")
                        .WithMany()
                        .HasForeignKey("TourClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimbaToursEastAfrica.Domain.Models.TourClient")
                        .WithMany()
                        .HasForeignKey("TourClientId1")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Location", b =>
                {
                    b.HasOne("SimbaToursEastAfrica.Domain.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Meal", b =>
                {
                    b.HasOne("SimbaToursEastAfrica.Domain.Models.MealPricing", "MealPricing")
                        .WithMany()
                        .HasForeignKey("MealPricingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimbaToursEastAfrica.Domain.Models.TourClient", "TourClient")
                        .WithMany()
                        .HasForeignKey("TourClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Schedule", b =>
                {
                    b.HasOne("SimbaToursEastAfrica.Domain.Models.Driver", "Driver")
                        .WithMany()
                        .HasForeignKey("DriverId")
                        .OnDelete(DeleteBehavior.Cascade);
                    

                    b.HasOne("SimbaToursEastAfrica.Domain.Models.Itinary", "Itinary")
                        .WithMany("Schedules")
                        .HasForeignKey("ItinaryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimbaToursEastAfrica.Domain.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade);
                    

                    b.HasOne("SimbaToursEastAfrica.Domain.Models.SchedulesPricing", "SchedulesPricing")
                        .WithMany()
                        .HasForeignKey("SchedulesId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimbaToursEastAfrica.Domain.Models.Vehicle", b =>
                {
                    b.HasOne("SimbaToursEastAfrica.Domain.Models.TourClient", "TourClient")
                        .WithMany("Vehicles")
                        .HasForeignKey("TourClientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
