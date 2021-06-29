using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryManagment.Data.Migrations
{
    public partial class SalaryBreakdown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NetAmount",
                table: "SalaryByMonth",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NetAmount",
                table: "SalaryByMonth");
        }
    }
}
