using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbaToursEastAfrica.Migrations
{
    public partial class inclusionOfItemPricing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "LaguagePricings",
                newName: "unitMealPrice");

            migrationBuilder.AddColumn<decimal>(
                name: "UnitLaguagePrice",
                table: "LaguagePricings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitMedicalPrice",
                table: "LaguagePricings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitTravelDocumentPrice",
                table: "LaguagePricings",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "laguagePricingId",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_laguagePricingId",
                table: "Items",
                column: "laguagePricingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_LaguagePricings_laguagePricingId",
                table: "Items",
                column: "laguagePricingId",
                principalTable: "LaguagePricings",
                principalColumn: "LaguagePricingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_LaguagePricings_laguagePricingId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_laguagePricingId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "UnitLaguagePrice",
                table: "LaguagePricings");

            migrationBuilder.DropColumn(
                name: "UnitMedicalPrice",
                table: "LaguagePricings");

            migrationBuilder.DropColumn(
                name: "UnitTravelDocumentPrice",
                table: "LaguagePricings");

            migrationBuilder.DropColumn(
                name: "laguagePricingId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "unitMealPrice",
                table: "LaguagePricings",
                newName: "Price");
        }
    }
}
