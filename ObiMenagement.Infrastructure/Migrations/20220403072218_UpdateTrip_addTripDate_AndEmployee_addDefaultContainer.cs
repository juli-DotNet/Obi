using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObiMenagement.Infrastructure.Migrations
{
    public partial class UpdateTrip_addTripDate_AndEmployee_addDefaultContainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TripDate",
                table: "Trip",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "DefaultTruckContainerId",
                table: "Employee",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employee_DefaultTruckContainerId",
                table: "Employee",
                column: "DefaultTruckContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_TruckContainer_DefaultTruckContainerId",
                table: "Employee",
                column: "DefaultTruckContainerId",
                principalTable: "TruckContainer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_TruckContainer_DefaultTruckContainerId",
                table: "Employee");

            migrationBuilder.DropIndex(
                name: "IX_Employee_DefaultTruckContainerId",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "TripDate",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "DefaultTruckContainerId",
                table: "Employee");
        }
    }
}
