using Microsoft.EntityFrameworkCore.Migrations;

namespace SalaryManagment.Data.Migrations
{
    public partial class salarytest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appraisals_Salarys_SalaryId",
                table: "Appraisals");

            migrationBuilder.DropForeignKey(
                name: "FK_Bonuss_SalaryByMonth_SalaryByMonthId",
                table: "Bonuss");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryByMonth_Salarys_SalaryId",
                table: "SalaryByMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryHistorys_SalaryByMonth_SalaryByMonthId",
                table: "SalaryHistorys");

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
                name: "SalaryByMonthId",
                table: "SalaryHistorys");

            migrationBuilder.DropColumn(
                name: "SalaryByMonthId",
                table: "Bonuss");

            migrationBuilder.DropColumn(
                name: "SalaryId",
                table: "Appraisals");

            migrationBuilder.RenameColumn(
                name: "SalaryId",
                table: "SalaryByMonth",
                newName: "SalaryHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_SalaryByMonth_SalaryId",
                table: "SalaryByMonth",
                newName: "IX_SalaryByMonth_SalaryHistoryId");

            migrationBuilder.AddColumn<int>(
                name: "AppraisalId",
                table: "Salarys",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SalaryHistoryId",
                table: "Salarys",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "SalaryByMonth",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BonusId",
                table: "SalaryByMonth",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Salarys_AppraisalId",
                table: "Salarys",
                column: "AppraisalId");

            migrationBuilder.CreateIndex(
                name: "IX_Salarys_SalaryHistoryId",
                table: "Salarys",
                column: "SalaryHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SalaryByMonth_BonusId",
                table: "SalaryByMonth",
                column: "BonusId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryByMonth_Bonuss_BonusId",
                table: "SalaryByMonth",
                column: "BonusId",
                principalTable: "Bonuss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryByMonth_SalaryHistorys_SalaryHistoryId",
                table: "SalaryByMonth",
                column: "SalaryHistoryId",
                principalTable: "SalaryHistorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salarys_Appraisals_AppraisalId",
                table: "Salarys",
                column: "AppraisalId",
                principalTable: "Appraisals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salarys_SalaryHistorys_SalaryHistoryId",
                table: "Salarys",
                column: "SalaryHistoryId",
                principalTable: "SalaryHistorys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalaryByMonth_Bonuss_BonusId",
                table: "SalaryByMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_SalaryByMonth_SalaryHistorys_SalaryHistoryId",
                table: "SalaryByMonth");

            migrationBuilder.DropForeignKey(
                name: "FK_Salarys_Appraisals_AppraisalId",
                table: "Salarys");

            migrationBuilder.DropForeignKey(
                name: "FK_Salarys_SalaryHistorys_SalaryHistoryId",
                table: "Salarys");

            migrationBuilder.DropIndex(
                name: "IX_Salarys_AppraisalId",
                table: "Salarys");

            migrationBuilder.DropIndex(
                name: "IX_Salarys_SalaryHistoryId",
                table: "Salarys");

            migrationBuilder.DropIndex(
                name: "IX_SalaryByMonth_BonusId",
                table: "SalaryByMonth");

            migrationBuilder.DropColumn(
                name: "AppraisalId",
                table: "Salarys");

            migrationBuilder.DropColumn(
                name: "SalaryHistoryId",
                table: "Salarys");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "SalaryByMonth");

            migrationBuilder.DropColumn(
                name: "BonusId",
                table: "SalaryByMonth");

            migrationBuilder.RenameColumn(
                name: "SalaryHistoryId",
                table: "SalaryByMonth",
                newName: "SalaryId");

            migrationBuilder.RenameIndex(
                name: "IX_SalaryByMonth_SalaryHistoryId",
                table: "SalaryByMonth",
                newName: "IX_SalaryByMonth_SalaryId");

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

            migrationBuilder.AddColumn<int>(
                name: "SalaryId",
                table: "Appraisals",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
                name: "FK_SalaryByMonth_Salarys_SalaryId",
                table: "SalaryByMonth",
                column: "SalaryId",
                principalTable: "Salarys",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SalaryHistorys_SalaryByMonth_SalaryByMonthId",
                table: "SalaryHistorys",
                column: "SalaryByMonthId",
                principalTable: "SalaryByMonth",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
