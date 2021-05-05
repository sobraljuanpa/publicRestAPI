using Microsoft.EntityFrameworkCore.Migrations;

namespace IDataAccess.Migrations
{
    public partial class AddConsultationProblem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Problems_ProblemId",
                table: "Consultations");

            migrationBuilder.AlterColumn<int>(
                name: "ProblemId",
                table: "Consultations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Problems_ProblemId",
                table: "Consultations",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Problems_ProblemId",
                table: "Consultations");

            migrationBuilder.AlterColumn<int>(
                name: "ProblemId",
                table: "Consultations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Problems_ProblemId",
                table: "Consultations",
                column: "ProblemId",
                principalTable: "Problems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
