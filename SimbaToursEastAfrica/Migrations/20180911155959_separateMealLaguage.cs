using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbaToursEastAfrica.Migrations
{
    public partial class separateMealLaguage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "mealPricingId",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Items_mealPricingId",
                table: "Items",
                column: "mealPricingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_MealPricings_mealPricingId",
                table: "Items",
                column: "mealPricingId",
                principalTable: "MealPricings",
                principalColumn: "MealPricingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_MealPricings_MealPricingId",
                table: "Meals",
                column: "MealPricingId",
                principalTable: "MealPricings",
                principalColumn: "MealPricingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_MealPricings_mealPricingId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Meals_MealPricings_MealPricingId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Items_mealPricingId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "mealPricingId",
                table: "Items");

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
    }
}
