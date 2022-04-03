using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ObiMenagement.Infrastructure.Migrations
{
    public partial class AddTripModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RoadDataId",
                table: "RoadClient",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Trip",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    TruckBaseId = table.Column<int>(type: "integer", nullable: false),
                    TruckContainerId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    StartingTrucKm = table.Column<long>(type: "bigint", nullable: false),
                    EndingTrucKm = table.Column<long>(type: "bigint", nullable: false),
                    TotalKM = table.Column<long>(type: "bigint", nullable: false),
                    StartingAmountOfFuel = table.Column<int>(type: "integer", nullable: false),
                    EndingAmountOfFuel = table.Column<int>(type: "integer", nullable: false),
                    TotalWage = table.Column<decimal>(type: "numeric", nullable: false),
                    IsValid = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trip_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trip_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Trip_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trip_TruckBase_TruckBaseId",
                        column: x => x.TruckBaseId,
                        principalTable: "TruckBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trip_TruckContainer_TruckContainerId",
                        column: x => x.TruckContainerId,
                        principalTable: "TruckContainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoadData",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TruckBaseId = table.Column<int>(type: "integer", nullable: false),
                    TruckContainerId = table.Column<int>(type: "integer", nullable: false),
                    StartingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndingDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartingLocationId = table.Column<int>(type: "integer", nullable: false),
                    DestinationLocationId = table.Column<int>(type: "integer", nullable: false),
                    TotalKM = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    IsExport = table.Column<bool>(type: "boolean", nullable: false),
                    TripId = table.Column<long>(type: "bigint", nullable: true),
                    IsValid = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoadData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoadData_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoadData_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoadData_Location_DestinationLocationId",
                        column: x => x.DestinationLocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoadData_Location_StartingLocationId",
                        column: x => x.StartingLocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoadData_Trip_TripId",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoadData_TruckBase_TruckBaseId",
                        column: x => x.TruckBaseId,
                        principalTable: "TruckBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoadData_TruckContainer_TruckContainerId",
                        column: x => x.TruckContainerId,
                        principalTable: "TruckContainer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoadExpense",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExpenseTypeId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    Payment_CurrencyId = table.Column<int>(type: "integer", nullable: false),
                    Payment_PaymentTypeEnum = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false),
                    CountryId = table.Column<int>(type: "integer", nullable: false),
                    RoadDataId = table.Column<long>(type: "bigint", nullable: true),
                    TripId = table.Column<long>(type: "bigint", nullable: true),
                    IsValid = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedById = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoadExpense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoadExpense_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoadExpense_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoadExpense_Country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoadExpense_Currency_Payment_CurrencyId",
                        column: x => x.Payment_CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoadExpense_ExpenseType_ExpenseTypeId",
                        column: x => x.ExpenseTypeId,
                        principalTable: "ExpenseType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoadExpense_RoadData_RoadDataId",
                        column: x => x.RoadDataId,
                        principalTable: "RoadData",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RoadExpense_Trip_TripId",
                        column: x => x.TripId,
                        principalTable: "Trip",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoadClient_RoadDataId",
                table: "RoadClient",
                column: "RoadDataId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadData_CreatedById",
                table: "RoadData",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RoadData_DestinationLocationId",
                table: "RoadData",
                column: "DestinationLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadData_ModifiedById",
                table: "RoadData",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RoadData_StartingLocationId",
                table: "RoadData",
                column: "StartingLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadData_TripId",
                table: "RoadData",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadData_TruckBaseId",
                table: "RoadData",
                column: "TruckBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadData_TruckContainerId",
                table: "RoadData",
                column: "TruckContainerId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadExpense_CountryId",
                table: "RoadExpense",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadExpense_CreatedById",
                table: "RoadExpense",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_RoadExpense_ExpenseTypeId",
                table: "RoadExpense",
                column: "ExpenseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadExpense_ModifiedById",
                table: "RoadExpense",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_RoadExpense_Payment_CurrencyId",
                table: "RoadExpense",
                column: "Payment_CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadExpense_RoadDataId",
                table: "RoadExpense",
                column: "RoadDataId");

            migrationBuilder.CreateIndex(
                name: "IX_RoadExpense_TripId",
                table: "RoadExpense",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_CreatedById",
                table: "Trip",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_EmployeeId",
                table: "Trip",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_ModifiedById",
                table: "Trip",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_TruckBaseId",
                table: "Trip",
                column: "TruckBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Trip_TruckContainerId",
                table: "Trip",
                column: "TruckContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoadClient_RoadData_RoadDataId",
                table: "RoadClient",
                column: "RoadDataId",
                principalTable: "RoadData",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoadClient_RoadData_RoadDataId",
                table: "RoadClient");

            migrationBuilder.DropTable(
                name: "RoadExpense");

            migrationBuilder.DropTable(
                name: "RoadData");

            migrationBuilder.DropTable(
                name: "Trip");

            migrationBuilder.DropIndex(
                name: "IX_RoadClient_RoadDataId",
                table: "RoadClient");

            migrationBuilder.DropColumn(
                name: "RoadDataId",
                table: "RoadClient");
        }
    }
}
