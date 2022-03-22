using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ObiMenagement.Infrastructure.Migrations
{
    public partial class RenameIsPositiveColument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPositive",
                table: "ExpenseType",
                newName: "IsPrepaymentGivenToEmployees");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPrepaymentGivenToEmployees",
                table: "ExpenseType",
                newName: "IsPositive");
        }
    }
}
