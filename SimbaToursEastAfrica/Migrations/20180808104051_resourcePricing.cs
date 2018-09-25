using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbaToursEastAfrica.Migrations
{
    public partial class resourcePricing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "SchedulesId",
                table: "Schedules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SchedulesPricingId",
                table: "Schedules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MealPricingId",
                table: "Meals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LaguagePricingId",
                table: "Laguages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TourClientId1",
                table: "Laguages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HotelPricingId",
                table: "HotelBookings",
                nullable: false,
                defaultValue: 0);

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
                name: "LaguagePricings",
                columns: table => new
                {
                    LaguagePricingId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LaguagePricingName = table.Column<string>(nullable: true),
                    LaguageDescription = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
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
                    SchedulesDescription = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchedulesPricings", x => x.SchedulesPricingId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_SchedulesId",
                table: "Schedules",
                column: "SchedulesId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_MealPricingId",
                table: "Meals",
                column: "MealPricingId");

            migrationBuilder.CreateIndex(
                name: "IX_Laguages_LaguagePricingId",
                table: "Laguages",
                column: "LaguagePricingId");

            migrationBuilder.CreateIndex(
                name: "IX_Laguages_TourClientId1",
                table: "Laguages",
                column: "TourClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_HotelBookings_HotelPricingId",
                table: "HotelBookings",
                column: "HotelPricingId");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelBookings_HotelPricings_HotelPricingId",
                table: "HotelBookings",
                column: "HotelPricingId",
                principalTable: "HotelPricings",
                principalColumn: "HotelPricingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Laguages_LaguagePricings_LaguagePricingId",
                table: "Laguages",
                column: "LaguagePricingId",
                principalTable: "LaguagePricings",
                principalColumn: "LaguagePricingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Laguages_TourClients_TourClientId1",
                table: "Laguages",
                column: "TourClientId1",
                principalTable: "TourClients",
                principalColumn: "TourClientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealPricings_MealPricingId",
                table: "Meals",
                column: "MealPricingId",
                principalTable: "MealPricings",
                principalColumn: "MealPricingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_SchedulesPricings_SchedulesId",
                table: "Schedules",
                column: "SchedulesId",
                principalTable: "SchedulesPricings",
                principalColumn: "SchedulesPricingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelBookings_HotelPricings_HotelPricingId",
                table: "HotelBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Laguages_LaguagePricings_LaguagePricingId",
                table: "Laguages");

            migrationBuilder.DropForeignKey(
                name: "FK_Laguages_TourClients_TourClientId1",
                table: "Laguages");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealPricings_MealPricingId",
                table: "Meals");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_SchedulesPricings_SchedulesId",
                table: "Schedules");

            migrationBuilder.DropTable(
                name: "DealsPricings");

            migrationBuilder.DropTable(
                name: "HotelPricings");

            migrationBuilder.DropTable(
                name: "LaguagePricings");

            migrationBuilder.DropTable(
                name: "MealPricings");

            migrationBuilder.DropTable(
                name: "SchedulesPricings");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_SchedulesId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Meals_MealPricingId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Laguages_LaguagePricingId",
                table: "Laguages");

            migrationBuilder.DropIndex(
                name: "IX_Laguages_TourClientId1",
                table: "Laguages");

            migrationBuilder.DropIndex(
                name: "IX_HotelBookings_HotelPricingId",
                table: "HotelBookings");

            migrationBuilder.DropColumn(
                name: "SchedulesId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "SchedulesPricingId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "MealPricingId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "LaguagePricingId",
                table: "Laguages");

            migrationBuilder.DropColumn(
                name: "TourClientId1",
                table: "Laguages");

            migrationBuilder.DropColumn(
                name: "HotelPricingId",
                table: "HotelBookings");
        }
    }
}
