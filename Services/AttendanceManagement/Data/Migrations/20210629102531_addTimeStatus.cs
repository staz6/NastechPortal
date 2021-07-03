using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceManagement.Data.Migrations
{
    public partial class addTimeStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeStatus",
                table: "Attendances",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStatus",
                table: "Attendances");
        }
    }
}
