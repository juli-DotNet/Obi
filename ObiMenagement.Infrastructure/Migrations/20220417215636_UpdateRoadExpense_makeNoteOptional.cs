using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObiMenagement.Infrastructure.Migrations
{
    public partial class UpdateRoadExpense_makeNoteOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoadData_Trip_TripId",
                table: "RoadData");

            migrationBuilder.DropForeignKey(
                name: "FK_RoadExpense_Country_CountryId",
                table: "RoadExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_RoadExpense_Trip_TripId",
                table: "RoadExpense");

            migrationBuilder.AlterColumn<long>(
                name: "TripId",
                table: "RoadExpense",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "RoadExpense",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "RoadExpense",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<long>(
                name: "TripId",
                table: "RoadData",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoadData_Trip_TripId",
                table: "RoadData",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoadExpense_Country_CountryId",
                table: "RoadExpense",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoadExpense_Trip_TripId",
                table: "RoadExpense",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoadData_Trip_TripId",
                table: "RoadData");

            migrationBuilder.DropForeignKey(
                name: "FK_RoadExpense_Country_CountryId",
                table: "RoadExpense");

            migrationBuilder.DropForeignKey(
                name: "FK_RoadExpense_Trip_TripId",
                table: "RoadExpense");

            migrationBuilder.AlterColumn<long>(
                name: "TripId",
                table: "RoadExpense",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "RoadExpense",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "RoadExpense",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "TripId",
                table: "RoadData",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_RoadData_Trip_TripId",
                table: "RoadData",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoadExpense_Country_CountryId",
                table: "RoadExpense",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoadExpense_Trip_TripId",
                table: "RoadExpense",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id");
        }
    }
}
