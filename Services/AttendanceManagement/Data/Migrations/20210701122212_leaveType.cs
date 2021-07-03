using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceManagement.Data.Migrations
{
    public partial class leaveType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeaveType",
                table: "Leaves",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeaveType",
                table: "Leaves");
        }
    }
}
