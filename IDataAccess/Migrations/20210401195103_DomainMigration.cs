using Microsoft.EntityFrameworkCore.Migrations;

namespace IDataAccess.Migrations
{
    public partial class DomainMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProblemId",
                table: "Consultations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PsychologistId",
                table: "Consultations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_ProblemId",
                table: "Consultations",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_PsychologistId",
                table: "Consultations",
                column: "PsychologistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Problems_ProblemId",
                table: "Consultations",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Psychologists_PsychologistId",
                table: "Consultations",
                column: "PsychologistId",
                principalTable: "Psychologists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Problems_ProblemId",
                table: "Consultations");

            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Psychologists_PsychologistId",
                table: "Consultations");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_ProblemId",
                table: "Consultations");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_PsychologistId",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "ProblemId",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "PsychologistId",
                table: "Consultations");
        }
    }
}
