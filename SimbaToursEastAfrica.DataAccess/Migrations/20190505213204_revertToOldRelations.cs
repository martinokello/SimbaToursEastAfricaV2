using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbaToursEastAfrica.DataAccess.Migrations
{
    public partial class revertToOldRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourClientExtraCharges_TourClients_TourClientExtraChargesId",
                table: "TourClientExtraCharges");

            migrationBuilder.AddColumn<int>(
                name: "HotelId",
                table: "TourClients",
                nullable: false,
                defaultValue: 0);
            
            migrationBuilder.CreateIndex(
                name: "IX_TourClients_HotelId",
                table: "TourClients",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_TourClients_Hotels_HotelId",
                table: "TourClients",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "HotelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TourClients_Hotels_HotelId",
                table: "TourClients");

            migrationBuilder.DropIndex(
                name: "IX_TourClients_HotelId",
                table: "TourClients");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "TourClients");

            migrationBuilder.AlterColumn<int>(
                name: "TourClientExtraChargesId",
                table: "TourClientExtraCharges",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_TourClientExtraCharges_TourClients_TourClientExtraChargesId",
                table: "TourClientExtraCharges",
                column: "TourClientExtraChargesId",
                principalTable: "TourClients",
                principalColumn: "TourClientId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
