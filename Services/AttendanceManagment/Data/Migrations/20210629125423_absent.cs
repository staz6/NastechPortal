using Microsoft.EntityFrameworkCore.Migrations;

namespace AttendanceManagment.Data.Migrations
{
    public partial class absent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStatus",
                table: "Attendances");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TimeStatus",
                table: "Attendances",
                type: "TEXT",
                nullable: true);
        }
    }
}
