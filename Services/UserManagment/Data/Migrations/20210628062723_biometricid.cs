using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManagment.Data.Migrations
{
    public partial class biometricid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BioMetricId",
                table: "Employees",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BioMetricId",
                table: "Employees");
        }
    }
}
