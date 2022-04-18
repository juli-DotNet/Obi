using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObiMenagement.Infrastructure.Migrations
{
    public partial class AddPriceOnTheMainModelNotOnValueObject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DefaultPayment_Price",
                table: "ExpenseType",
                newName: "Price");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ExpenseType",
                newName: "DefaultPayment_Price");
        }
    }
}
