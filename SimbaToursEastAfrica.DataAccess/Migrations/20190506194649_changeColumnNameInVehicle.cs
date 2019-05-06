using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbaToursEastAfrica.DataAccess.Migrations
{
    public partial class changeColumnNameInVehicle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AcutualNumberOfPassengersAllocated",
                table: "Vehicles",
                newName: "ActualNumberOfPassengersAllocated");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ActualNumberOfPassengersAllocated",
                table: "Vehicles",
                newName: "AcutualNumberOfPassengersAllocated");
        }
    }
}
