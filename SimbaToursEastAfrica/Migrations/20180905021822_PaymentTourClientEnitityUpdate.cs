using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimbaToursEastAfrica.Migrations
{
    public partial class PaymentTourClientEnitityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "CurrentPayment",
                table: "TourClients",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "TourClients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateUpdated",
                table: "TourClients",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "TourClients",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasFullyPaid",
                table: "TourClients",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "PaidInstallments",
                table: "TourClients",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPayment",
                table: "TourClients");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "TourClients");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "TourClients");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "TourClients");

            migrationBuilder.DropColumn(
                name: "HasFullyPaid",
                table: "TourClients");

            migrationBuilder.DropColumn(
                name: "PaidInstallments",
                table: "TourClients");
        }
    }
}
