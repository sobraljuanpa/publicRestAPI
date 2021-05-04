using Microsoft.EntityFrameworkCore.Migrations;

namespace IDataAccess.Migrations
{
    public partial class AddedDefaultProblems : Migration
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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Problems",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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

            migrationBuilder.InsertData(
                table: "Problems",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Depresión" },
                    { 2, "Estrés" },
                    { 3, "Ansiedad" },
                    { 4, "Autoestima" },
                    { 5, "Enojo" },
                    { 6, "Relaciones" },
                    { 7, "Duelo" },
                    { 8, "Y más" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Psychologists_ScheduleId",
                table: "Psychologists",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Problems_Name",
                table: "Problems",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

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

            migrationBuilder.DropIndex(
                name: "IX_Problems_Name",
                table: "Problems");

            migrationBuilder.DeleteData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Problems",
                keyColumn: "Id",
                keyValue: 8);

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

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Problems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

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
