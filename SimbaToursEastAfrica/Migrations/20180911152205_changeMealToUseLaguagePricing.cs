using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbaToursEastAfrica.Migrations
{
    public partial class changeMealToUseLaguagePricing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealPricings_MealPricingId",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "MealPricingId",
                table: "Meals",
                newName: "LaguagePricingId");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_MealPricingId",
                table: "Meals",
                newName: "IX_Meals_LaguagePricingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_LaguagePricings_LaguagePricingId",
                table: "Meals",
                column: "LaguagePricingId",
                principalTable: "LaguagePricings",
                principalColumn: "LaguagePricingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_LaguagePricings_LaguagePricingId",
                table: "Meals");

            migrationBuilder.RenameColumn(
                name: "LaguagePricingId",
                table: "Meals",
                newName: "MealPricingId");

            migrationBuilder.RenameIndex(
                name: "IX_Meals_LaguagePricingId",
                table: "Meals",
                newName: "IX_Meals_MealPricingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealPricings_MealPricingId",
                table: "Meals",
                column: "MealPricingId",
                principalTable: "MealPricings",
                principalColumn: "MealPricingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
