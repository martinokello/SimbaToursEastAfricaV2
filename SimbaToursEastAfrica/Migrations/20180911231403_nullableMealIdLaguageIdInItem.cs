using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbaToursEastAfrica.Migrations
{
    public partial class nullableMealIdLaguageIdInItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_LaguagePricings_laguagePricingId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_MealPricings_mealPricingId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "mealPricingId",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "laguagePricingId",
                table: "Items",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Items_LaguagePricings_laguagePricingId",
                table: "Items",
                column: "laguagePricingId",
                principalTable: "LaguagePricings",
                principalColumn: "LaguagePricingId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_MealPricings_mealPricingId",
                table: "Items",
                column: "mealPricingId",
                principalTable: "MealPricings",
                principalColumn: "MealPricingId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_LaguagePricings_laguagePricingId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_MealPricings_mealPricingId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "mealPricingId",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "laguagePricingId",
                table: "Items",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_LaguagePricings_laguagePricingId",
                table: "Items",
                column: "laguagePricingId",
                principalTable: "LaguagePricings",
                principalColumn: "LaguagePricingId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_MealPricings_mealPricingId",
                table: "Items",
                column: "mealPricingId",
                principalTable: "MealPricings",
                principalColumn: "MealPricingId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
