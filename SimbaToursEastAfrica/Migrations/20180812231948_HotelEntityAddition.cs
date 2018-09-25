using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbaToursEastAfrica.Migrations
{
    public partial class HotelEntityAddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelPricingId",
                table: "Hotels",
                column: "HotelPricingId");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_LocationId",
                table: "Hotels",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hotels");
        }
    }
}
