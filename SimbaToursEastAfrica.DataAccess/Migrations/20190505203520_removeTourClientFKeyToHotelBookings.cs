using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbaToursEastAfrica.DataAccess.Migrations
{
    public partial class removeTourClientFKeyToHotelBookings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourClients_HotelBookings_TourClientId",
                table: "TourClients");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_TourClients_HotelBookings_TourClientId",
                table: "TourClients",
                column: "TourClientId",
                principalTable: "HotelBookings",
                principalColumn: "HotelBookingId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
