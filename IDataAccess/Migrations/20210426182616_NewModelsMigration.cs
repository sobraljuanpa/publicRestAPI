using Microsoft.EntityFrameworkCore.Migrations;

namespace IDataAccess.Migrations
{
    public partial class NewModelsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Problems_Psychologists_PsychologistId",
                table: "Problems");

            migrationBuilder.DropIndex(
                name: "IX_Problems_PsychologistId",
                table: "Problems");

            migrationBuilder.DropColumn(
                name: "PsychologistId",
                table: "Problems");

            migrationBuilder.AddColumn<int>(
                name: "ActiveYears",
                table: "Psychologists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ScheduleId",
                table: "Psychologists",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Consultations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Date",
                table: "Consultations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemote",
                table: "Consultations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ProblemPsychologist",
                columns: table => new
                {
                    ExpertiseId = table.Column<int>(type: "int", nullable: false),
                    SpecialistsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProblemPsychologist", x => new { x.ExpertiseId, x.SpecialistsId });
                    table.ForeignKey(
                        name: "FK_ProblemPsychologist_Problems_ExpertiseId",
                        column: x => x.ExpertiseId,
                        principalTable: "Problems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProblemPsychologist_Psychologists_SpecialistsId",
                        column: x => x.SpecialistsId,
                        principalTable: "Psychologists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MondayConsultations = table.Column<int>(type: "int", nullable: false),
                    TuesdayConsultations = table.Column<int>(type: "int", nullable: false),
                    WednesdayConsultations = table.Column<int>(type: "int", nullable: false),
                    ThursdayConsultations = table.Column<int>(type: "int", nullable: false),
                    FridayConsultations = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Psychologists_ScheduleId",
                table: "Psychologists",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_ProblemPsychologist_SpecialistsId",
                table: "ProblemPsychologist",
                column: "SpecialistsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Psychologists_Schedules_ScheduleId",
                table: "Psychologists",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Psychologists_Schedules_ScheduleId",
                table: "Psychologists");

            migrationBuilder.DropTable(
                name: "ProblemPsychologist");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Psychologists_ScheduleId",
                table: "Psychologists");

            migrationBuilder.DropColumn(
                name: "ActiveYears",
                table: "Psychologists");

            migrationBuilder.DropColumn(
                name: "ScheduleId",
                table: "Psychologists");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "IsRemote",
                table: "Consultations");

            migrationBuilder.AddColumn<int>(
                name: "PsychologistId",
                table: "Problems",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Problems_PsychologistId",
                table: "Problems",
                column: "PsychologistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Problems_Psychologists_PsychologistId",
                table: "Problems",
                column: "PsychologistId",
                principalTable: "Psychologists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
