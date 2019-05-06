using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbaToursEastAfrica.DataAccess.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    AddressId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AddressLine1 = table.Column<string>(nullable: true),
                    AddressLine2 = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Town = table.Column<string>(nullable: true),
                    PostCode = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.AddressId);
                });

            migrationBuilder.CreateTable(
                name: "DealsPricings",
                columns: table => new
                {
                    DealsPricingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DealName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DealsPricings", x => x.DealsPricingId);
                });

            migrationBuilder.CreateTable(
                name: "Destinations",
                columns: table => new
                {
                    DestinationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DestinationFrom = table.Column<string>(nullable: true),
                    DestinationTo = table.Column<string>(nullable: true),
                    DepartTime = table.Column<DateTime>(nullable: false),
                    ArriveTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinations", x => x.DestinationId);
                });

            migrationBuilder.CreateTable(
                name: "HotelPricings",
                columns: table => new
                {
                    HotelPricingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Price = table.Column<decimal>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelPricings", x => x.HotelPricingId);
                });

            migrationBuilder.CreateTable(
                name: "Itinaries",
                columns: table => new
                {
                    ItinaryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itinaries", x => x.ItinaryId);
                });

            migrationBuilder.CreateTable(
                name: "LaguagePricings",
                columns: table => new
                {
                    LaguagePricingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LaguagePricingName = table.Column<string>(nullable: true),
                    LaguageDescription = table.Column<string>(nullable: true),
                    UnitLaguagePrice = table.Column<decimal>(nullable: false),
                    UnitMedicalPrice = table.Column<decimal>(nullable: false),
                    UnitTravelDocumentPrice = table.Column<decimal>(nullable: false),
                    unitMealPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaguagePricings", x => x.LaguagePricingId);
                });

            migrationBuilder.CreateTable(
                name: "MealPricings",
                columns: table => new
                {
                    MealPricingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MealName = table.Column<string>(nullable: true),
                    MealDescription = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealPricings", x => x.MealPricingId);
                });

            migrationBuilder.CreateTable(
                name: "SchedulesPricings",
                columns: table => new
                {
                    SchedulesPricingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SchedulesPricingName = table.Column<string>(nullable: true),
                    SchedulesDescription = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulesPricings", x => x.SchedulesPricingId);
                });

            migrationBuilder.CreateTable(
                name: "TransportPricings",
                columns: table => new
                {
                    TransportPricingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TransportPricingName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    FourByFourPricing = table.Column<decimal>(nullable: false),
                    MiniBusPricing = table.Column<decimal>(nullable: false),
                    TaxiPricing = table.Column<decimal>(nullable: false),
                    PickupTruckPricing = table.Column<decimal>(nullable: false),
                    TourBusPricing = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportPricings", x => x.TransportPricingId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Country = table.Column<string>(nullable: true),
                    AddressId = table.Column<int>(nullable: false),
                    LocationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                    table.ForeignKey(
                        name: "FK_Locations_Addresses_AddressId",
                        column: x => x.AddressId,
                        principalTable: "Addresses",
                        principalColumn: "AddressId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hotels",
                columns: table => new
                {
                    HotelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelName = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    HasMealsIncluded = table.Column<bool>(nullable: false),
                    HotelPricingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hotels", x => x.HotelId);
                    table.ForeignKey(
                        name: "FK_Hotels_HotelPricings_HotelPricingId",
                        column: x => x.HotelPricingId,
                        principalTable: "HotelPricings",
                        principalColumn: "HotelPricingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Hotels_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InAndOutBoundAirTravels",
                columns: table => new
                {
                    InAndOutBoundAirTravelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FlightNumber = table.Column<string>(nullable: true),
                    DestinationId = table.Column<int>(nullable: false),
                    FlightCost = table.Column<decimal>(nullable: false),
                    LaguageId = table.Column<int>(nullable: false),
                    HasMealsIncluded = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InAndOutBoundAirTravels", x => x.InAndOutBoundAirTravelId);
                    table.ForeignKey(
                        name: "FK_InAndOutBoundAirTravels_Destinations_DestinationId",
                        column: x => x.DestinationId,
                        principalTable: "Destinations",
                        principalColumn: "DestinationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    ScheduleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DriverId = table.Column<int>(nullable: false),
                    ItinaryId = table.Column<int>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    Operation = table.Column<string>(nullable: true),
                    TimeFrom = table.Column<DateTime>(nullable: false),
                    TimeTo = table.Column<DateTime>(nullable: false),
                    SchedulesId = table.Column<int>(nullable: false),
                    SchedulesPricingId = table.Column<int>(nullable: false),
                    DriverId1 = table.Column<int>(nullable: true),
                    ItinaryId1 = table.Column<int>(nullable: true),
                    LocationId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.ScheduleId);
                    table.ForeignKey(
                        name: "FK_Schedules_Itinaries_ItinaryId",
                        column: x => x.ItinaryId,
                        principalTable: "Itinaries",
                        principalColumn: "ItinaryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Itinaries_ItinaryId1",
                        column: x => x.ItinaryId1,
                        principalTable: "Itinaries",
                        principalColumn: "ItinaryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Schedules_Locations_LocationId1",
                        column: x => x.LocationId1,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Schedules_SchedulesPricings_SchedulesId",
                        column: x => x.SchedulesId,
                        principalTable: "SchedulesPricings",
                        principalColumn: "SchedulesPricingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourClients",
                columns: table => new
                {
                    TourClientId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientFirstName = table.Column<string>(nullable: true),
                    ClientLastName = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    HasRequiredVisaStatus = table.Column<bool>(nullable: false),
                    NumberOfIndividuals = table.Column<int>(nullable: false),
                    GrossTotalCosts = table.Column<decimal>(nullable: false),
                    HotelId = table.Column<int>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    HasFullyPaid = table.Column<bool>(nullable: false),
                    PaidInstallments = table.Column<decimal>(nullable: false),
                    CurrentPayment = table.Column<decimal>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateUpdated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourClients", x => x.TourClientId);
                    table.ForeignKey(
                        name: "FK_TourClients_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HotelBookings",
                columns: table => new
                {
                    HotelBookingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    HotelName = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: false),
                    TourClientId = table.Column<int>(nullable: false),
                    AccomodationCost = table.Column<decimal>(nullable: false),
                    HasMealsIncluded = table.Column<bool>(nullable: false),
                    HotelPricingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelBookings", x => x.HotelBookingId);
                    table.ForeignKey(
                        name: "FK_HotelBookings_HotelPricings_HotelPricingId",
                        column: x => x.HotelPricingId,
                        principalTable: "HotelPricings",
                        principalColumn: "HotelPricingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelBookings_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HotelBookings_TourClients_TourClientId",
                        column: x => x.TourClientId,
                        principalTable: "TourClients",
                        principalColumn: "TourClientId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    InvoiceName = table.Column<string>(nullable: true),
                    NetCost = table.Column<decimal>(nullable: false),
                    PercentTaxAppliable = table.Column<decimal>(nullable: false),
                    GrossCost = table.Column<decimal>(nullable: false),
                    TourClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_TourClients_TourClientId",
                        column: x => x.TourClientId,
                        principalTable: "TourClients",
                        principalColumn: "TourClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Laguages",
                columns: table => new
                {
                    LaguageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TourClientId = table.Column<int>(nullable: false),
                    LaguagePricingId = table.Column<int>(nullable: false),
                    TourClientId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laguages", x => x.LaguageId);
                    table.ForeignKey(
                        name: "FK_Laguages_LaguagePricings_LaguagePricingId",
                        column: x => x.LaguagePricingId,
                        principalTable: "LaguagePricings",
                        principalColumn: "LaguagePricingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Laguages_TourClients_TourClientId",
                        column: x => x.TourClientId,
                        principalTable: "TourClients",
                        principalColumn: "TourClientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Laguages_TourClients_TourClientId1",
                        column: x => x.TourClientId1,
                        principalTable: "TourClients",
                        principalColumn: "TourClientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    MealId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TourClientId = table.Column<int>(nullable: false),
                    MealPricingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.MealId);
                    table.ForeignKey(
                        name: "FK_Meals_MealPricings_MealPricingId",
                        column: x => x.MealPricingId,
                        principalTable: "MealPricings",
                        principalColumn: "MealPricingId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Meals_TourClients_TourClientId",
                        column: x => x.TourClientId,
                        principalTable: "TourClients",
                        principalColumn: "TourClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TourClientExtraCharges",
                columns: table => new
                {
                    TourClientExtraChargesId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TourClientId = table.Column<int>(nullable: false),
                    ExtraCharges = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourClientExtraCharges", x => x.TourClientExtraChargesId);
                    table.ForeignKey(
                        name: "FK_TourClientExtraCharges_TourClients_TourClientExtraChargesId",
                        column: x => x.TourClientExtraChargesId,
                        principalTable: "TourClients",
                        principalColumn: "TourClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TourClientExtraCharges_TourClients_TourClientId",
                        column: x => x.TourClientId,
                        principalTable: "TourClients",
                        principalColumn: "TourClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    VehicleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicleRegistration = table.Column<string>(nullable: true),
                    MaxNumberOfPassengers = table.Column<int>(nullable: false),
                    AcutualNumberOfPassengersAllocated = table.Column<int>(nullable: false),
                    VehicleType = table.Column<int>(nullable: false),
                    TourClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.VehicleId);
                    table.ForeignKey(
                        name: "FK_Vehicles_TourClients_TourClientId",
                        column: x => x.TourClientId,
                        principalTable: "TourClients",
                        principalColumn: "TourClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ItemType = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ItemCost = table.Column<decimal>(nullable: false),
                    LaguageId = table.Column<int>(nullable: true),
                    MealId = table.Column<int>(nullable: true),
                    InvoiceId = table.Column<int>(nullable: false),
                    laguagePricingId = table.Column<int>(nullable: true),
                    mealPricingId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_Invoices_InvoiceId",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "InvoiceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Laguages_LaguageId",
                        column: x => x.LaguageId,
                        principalTable: "Laguages",
                        principalColumn: "LaguageId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_LaguagePricings_laguagePricingId",
                        column: x => x.laguagePricingId,
                        principalTable: "LaguagePricings",
                        principalColumn: "LaguagePricingId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Items_MealPricings_mealPricingId",
                        column: x => x.mealPricingId,
                        principalTable: "MealPricings",
                        principalColumn: "MealPricingId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    VehicleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverId);
                    table.ForeignKey(
                        name: "FK_Drivers_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternalVehicleTravels",
                columns: table => new
                {
                    InternalVehicleTravelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    VehicleId = table.Column<int>(nullable: false),
                    VehicleCosts = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalVehicleTravels", x => x.InternalVehicleTravelId);
                    table.ForeignKey(
                        name: "FK_InternalVehicleTravels_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "VehicleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drivers_VehicleId",
                table: "Drivers",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBookings_HotelPricingId",
                table: "HotelBookings",
                column: "HotelPricingId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBookings_LocationId",
                table: "HotelBookings",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBookings_TourClientId",
                table: "HotelBookings",
                column: "TourClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelPricingId",
                table: "Hotels",
                column: "HotelPricingId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_LocationId",
                table: "Hotels",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InAndOutBoundAirTravels_DestinationId",
                table: "InAndOutBoundAirTravels",
                column: "DestinationId");

            migrationBuilder.CreateIndex(
                name: "IX_InAndOutBoundAirTravels_LaguageId",
                table: "InAndOutBoundAirTravels",
                column: "LaguageId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalVehicleTravels_VehicleId",
                table: "InternalVehicleTravels",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_TourClientId",
                table: "Invoices",
                column: "TourClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_InvoiceId",
                table: "Items",
                column: "InvoiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_LaguageId",
                table: "Items",
                column: "LaguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_MealId",
                table: "Items",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_laguagePricingId",
                table: "Items",
                column: "laguagePricingId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_mealPricingId",
                table: "Items",
                column: "mealPricingId");

            migrationBuilder.CreateIndex(
                name: "IX_Laguages_LaguagePricingId",
                table: "Laguages",
                column: "LaguagePricingId");

            migrationBuilder.CreateIndex(
                name: "IX_Laguages_TourClientId",
                table: "Laguages",
                column: "TourClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Laguages_TourClientId1",
                table: "Laguages",
                column: "TourClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_AddressId",
                table: "Locations",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_MealPricingId",
                table: "Meals",
                column: "MealPricingId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_TourClientId",
                table: "Meals",
                column: "TourClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DriverId",
                table: "Schedules",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_DriverId1",
                table: "Schedules",
                column: "DriverId1");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ItinaryId",
                table: "Schedules",
                column: "ItinaryId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_ItinaryId1",
                table: "Schedules",
                column: "ItinaryId1");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_LocationId",
                table: "Schedules",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_LocationId1",
                table: "Schedules",
                column: "LocationId1");

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SchedulesId",
                table: "Schedules",
                column: "SchedulesId");

            migrationBuilder.CreateIndex(
                name: "IX_TourClientExtraCharges_TourClientId",
                table: "TourClientExtraCharges",
                column: "TourClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TourClients_HotelId",
                table: "TourClients",
                column: "HotelId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_TourClientId",
                table: "Vehicles",
                column: "TourClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_InAndOutBoundAirTravels_Laguages_LaguageId",
                table: "InAndOutBoundAirTravels",
                column: "LaguageId",
                principalTable: "Laguages",
                principalColumn: "LaguageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Drivers_DriverId",
                table: "Schedules",
                column: "DriverId",
                principalTable: "Drivers",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Drivers_DriverId1",
                table: "Schedules",
                column: "DriverId1",
                principalTable: "Drivers",
                principalColumn: "DriverId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TourClients_HotelBookings_TourClientId",
                table: "TourClients",
                column: "TourClientId",
                principalTable: "HotelBookings",
                principalColumn: "HotelBookingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelBookings_HotelPricings_HotelPricingId",
                table: "HotelBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_HotelPricings_HotelPricingId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelBookings_Locations_LocationId",
                table: "HotelBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Locations_LocationId",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelBookings_TourClients_TourClientId",
                table: "HotelBookings");

            migrationBuilder.DropTable(
                name: "DealsPricings");

            migrationBuilder.DropTable(
                name: "InAndOutBoundAirTravels");

            migrationBuilder.DropTable(
                name: "InternalVehicleTravels");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "TourClientExtraCharges");

            migrationBuilder.DropTable(
                name: "TransportPricings");

            migrationBuilder.DropTable(
                name: "Destinations");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Laguages");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Itinaries");

            migrationBuilder.DropTable(
                name: "SchedulesPricings");

            migrationBuilder.DropTable(
                name: "LaguagePricings");

            migrationBuilder.DropTable(
                name: "MealPricings");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "HotelPricings");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "TourClients");

            migrationBuilder.DropTable(
                name: "Hotels");

            migrationBuilder.DropTable(
                name: "HotelBookings");
        }
    }
}
