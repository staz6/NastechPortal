using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryManagment.Data.Migrations
{
    public partial class herewego : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Appraisals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Percent = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    Reason = table.Column<string>(type: "TEXT", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appraisals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bonuss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bonuss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restrictionss",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AllowedHoliday = table.Column<int>(type: "INTEGER", nullable: false),
                    AllowedLates = table.Column<int>(type: "INTEGER", nullable: false),
                    HolidayDeduction = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restrictionss", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryBreakdowns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DaySalary = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryBreakdowns", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryDeductions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", nullable: true),
                    DeductSalary = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryDeductions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryHistorys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryHistorys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SalaryByMonths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    Month = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Deduction = table.Column<int>(type: "INTEGER", nullable: false),
                    NetAmount = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    SalaryHistoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Status = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryByMonths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryByMonths_SalaryHistorys_SalaryHistoryId",
                        column: x => x.SalaryHistoryId,
                        principalTable: "SalaryHistorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Salarys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    Amount = table.Column<int>(type: "INTEGER", nullable: false),
                    SalaryBreakdownId = table.Column<int>(type: "INTEGER", nullable: true),
                    SalaryHistoryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salarys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salarys_SalaryBreakdowns_SalaryBreakdownId",
                        column: x => x.SalaryBreakdownId,
                        principalTable: "SalaryBreakdowns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Salarys_SalaryHistorys_SalaryHistoryId",
                        column: x => x.SalaryHistoryId,
                        principalTable: "SalaryHistorys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalaryByMonths_SalaryHistoryId",
                table: "SalaryByMonths",
                column: "SalaryHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Salarys_SalaryBreakdownId",
                table: "Salarys",
                column: "SalaryBreakdownId");

            migrationBuilder.CreateIndex(
                name: "IX_Salarys_SalaryHistoryId",
                table: "Salarys",
                column: "SalaryHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appraisals");

            migrationBuilder.DropTable(
                name: "Bonuss");

            migrationBuilder.DropTable(
                name: "Restrictionss");

            migrationBuilder.DropTable(
                name: "SalaryByMonths");

            migrationBuilder.DropTable(
                name: "SalaryDeductions");

            migrationBuilder.DropTable(
                name: "Salarys");

            migrationBuilder.DropTable(
                name: "SalaryBreakdowns");

            migrationBuilder.DropTable(
                name: "SalaryHistorys");
        }
    }
}
