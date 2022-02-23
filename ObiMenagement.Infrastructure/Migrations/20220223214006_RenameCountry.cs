using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ObiMenagement.Infrastructure.Migrations
{
    public partial class RenameCountry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_County_CountyId",
                table: "City");

            migrationBuilder.DropTable(
                name: "County");

            migrationBuilder.RenameColumn(
                name: "CountyId",
                table: "City",
                newName: "CountryId");

            migrationBuilder.RenameIndex(
                name: "IX_City_CountyId",
                table: "City",
                newName: "IX_City_CountryId");

            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "City",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "City",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedById",
                table: "City",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "City",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    IsValid = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Country_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Country_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_CreatedById",
                table: "City",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_City_ModifiedById",
                table: "City",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Country_CreatedById",
                table: "Country",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Country_ModifiedById",
                table: "Country",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_City_AspNetUsers_CreatedById",
                table: "City",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_City_AspNetUsers_ModifiedById",
                table: "City",
                column: "ModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_City_Country_CountryId",
                table: "City",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_City_AspNetUsers_CreatedById",
                table: "City");

            migrationBuilder.DropForeignKey(
                name: "FK_City_AspNetUsers_ModifiedById",
                table: "City");

            migrationBuilder.DropForeignKey(
                name: "FK_City_Country_CountryId",
                table: "City");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropIndex(
                name: "IX_City_CreatedById",
                table: "City");

            migrationBuilder.DropIndex(
                name: "IX_City_ModifiedById",
                table: "City");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "City");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "City");

            migrationBuilder.DropColumn(
                name: "ModifiedById",
                table: "City");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "City");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "City",
                newName: "CountyId");

            migrationBuilder.RenameIndex(
                name: "IX_City_CountryId",
                table: "City",
                newName: "IX_City_CountyId");

            migrationBuilder.CreateTable(
                name: "County",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IsValid = table.Column<bool>(type: "boolean", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_County", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_City_County_CountyId",
                table: "City",
                column: "CountyId",
                principalTable: "County",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
