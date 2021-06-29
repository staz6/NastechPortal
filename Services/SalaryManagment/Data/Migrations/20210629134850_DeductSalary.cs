using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryManagment.Data.Migrations
{
    public partial class DeductSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryByMonth_Bonuss_BonusId",
                table: "SalaryByMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryByMonth_SalaryDeductions_SalarayDeductionsId",
                table: "SalaryByMonth");

            migrationBuilder.DropIndex(
                name: "IX_SalaryByMonth_BonusId",
                table: "SalaryByMonth");

            migrationBuilder.DropIndex(
                name: "IX_SalaryByMonth_SalarayDeductionsId",
                table: "SalaryByMonth");

            migrationBuilder.DropColumn(
                name: "BonusId",
                table: "SalaryByMonth");

            migrationBuilder.DropColumn(
                name: "SalarayDeductionsId",
                table: "SalaryByMonth");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SalaryDeductions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Deduction",
                table: "SalaryByMonth",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Bonuss",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SalaryDeductions");

            migrationBuilder.DropColumn(
                name: "Deduction",
                table: "SalaryByMonth");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bonuss");

            migrationBuilder.AddColumn<int>(
                name: "BonusId",
                table: "SalaryByMonth",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalarayDeductionsId",
                table: "SalaryByMonth",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalaryByMonth_BonusId",
                table: "SalaryByMonth",
                column: "BonusId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryByMonth_SalarayDeductionsId",
                table: "SalaryByMonth",
                column: "SalarayDeductionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryByMonth_Bonuss_BonusId",
                table: "SalaryByMonth",
                column: "BonusId",
                principalTable: "Bonuss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryByMonth_SalaryDeductions_SalarayDeductionsId",
                table: "SalaryByMonth",
                column: "SalarayDeductionsId",
                principalTable: "SalaryDeductions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
