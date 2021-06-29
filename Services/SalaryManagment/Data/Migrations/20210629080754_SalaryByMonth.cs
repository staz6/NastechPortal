using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryManagment.Data.Migrations
{
    public partial class SalaryByMonth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SalaryPaid",
                table: "SalaryHistorys");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SalaryHistorys");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SalaryDeductions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SalaryBreakdowns");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Restrictionss");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bonuss");

            migrationBuilder.RenameColumn(
                name: "EmployeeSalary",
                table: "Salarys",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Appraisals",
                newName: "SalaryId");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Salarys",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "SalaryBreakdownId",
                table: "Salarys",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaryByMonthId",
                table: "SalaryHistorys",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaryByMonthId",
                table: "Bonuss",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SalaryByMonth",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Month = table.Column<string>(type: "TEXT", nullable: true),
                    SalarayDeductionsId = table.Column<int>(type: "INTEGER", nullable: true),
                    SalaryId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalaryByMonth", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SalaryByMonth_SalaryDeductions_SalarayDeductionsId",
                        column: x => x.SalarayDeductionsId,
                        principalTable: "SalaryDeductions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SalaryByMonth_Salarys_SalaryId",
                        column: x => x.SalaryId,
                        principalTable: "Salarys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Salarys_SalaryBreakdownId",
                table: "Salarys",
                column: "SalaryBreakdownId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryHistorys_SalaryByMonthId",
                table: "SalaryHistorys",
                column: "SalaryByMonthId");

            migrationBuilder.CreateIndex(
                name: "IX_Bonuss_SalaryByMonthId",
                table: "Bonuss",
                column: "SalaryByMonthId");

            migrationBuilder.CreateIndex(
                name: "IX_Appraisals_SalaryId",
                table: "Appraisals",
                column: "SalaryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalaryByMonth_SalarayDeductionsId",
                table: "SalaryByMonth",
                column: "SalarayDeductionsId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryByMonth_SalaryId",
                table: "SalaryByMonth",
                column: "SalaryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appraisals_Salarys_SalaryId",
                table: "Appraisals",
                column: "SalaryId",
                principalTable: "Salarys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bonuss_SalaryByMonth_SalaryByMonthId",
                table: "Bonuss",
                column: "SalaryByMonthId",
                principalTable: "SalaryByMonth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryHistorys_SalaryByMonth_SalaryByMonthId",
                table: "SalaryHistorys",
                column: "SalaryByMonthId",
                principalTable: "SalaryByMonth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salarys_SalaryBreakdowns_SalaryBreakdownId",
                table: "Salarys",
                column: "SalaryBreakdownId",
                principalTable: "SalaryBreakdowns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appraisals_Salarys_SalaryId",
                table: "Appraisals");

            migrationBuilder.DropForeignKey(
                name: "FK_Bonuss_SalaryByMonth_SalaryByMonthId",
                table: "Bonuss");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryHistorys_SalaryByMonth_SalaryByMonthId",
                table: "SalaryHistorys");

            migrationBuilder.DropForeignKey(
                name: "FK_Salarys_SalaryBreakdowns_SalaryBreakdownId",
                table: "Salarys");

            migrationBuilder.DropTable(
                name: "SalaryByMonth");

            migrationBuilder.DropIndex(
                name: "IX_Salarys_SalaryBreakdownId",
                table: "Salarys");

            migrationBuilder.DropIndex(
                name: "IX_SalaryHistorys_SalaryByMonthId",
                table: "SalaryHistorys");

            migrationBuilder.DropIndex(
                name: "IX_Bonuss_SalaryByMonthId",
                table: "Bonuss");

            migrationBuilder.DropIndex(
                name: "IX_Appraisals_SalaryId",
                table: "Appraisals");

            migrationBuilder.DropColumn(
                name: "SalaryBreakdownId",
                table: "Salarys");

            migrationBuilder.DropColumn(
                name: "SalaryByMonthId",
                table: "SalaryHistorys");

            migrationBuilder.DropColumn(
                name: "SalaryByMonthId",
                table: "Bonuss");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Salarys",
                newName: "EmployeeSalary");

            migrationBuilder.RenameColumn(
                name: "SalaryId",
                table: "Appraisals",
                newName: "UserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Salarys",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaryPaid",
                table: "SalaryHistorys",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SalaryHistorys",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SalaryDeductions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "SalaryBreakdowns",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Restrictionss",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Bonuss",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
