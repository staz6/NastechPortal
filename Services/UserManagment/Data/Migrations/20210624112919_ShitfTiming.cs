using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagment.Data.Migrations
{
    public partial class ShitfTiming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WorkingHours",
                table: "Employees",
                newName: "ShiftTiming");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShiftTiming",
                table: "Employees",
                newName: "WorkingHours");
        }
    }
}
