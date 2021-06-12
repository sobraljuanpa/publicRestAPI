using Microsoft.EntityFrameworkCore.Migrations;

namespace IDataAccess.Migrations
{
    public partial class ExplicitReference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Psychologists_Schedules_ScheduleId",
                table: "Psychologists");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "Psychologists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Psychologists_Schedules_ScheduleId",
                table: "Psychologists",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Psychologists_Schedules_ScheduleId",
                table: "Psychologists");

            migrationBuilder.AlterColumn<int>(
                name: "ScheduleId",
                table: "Psychologists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Psychologists_Schedules_ScheduleId",
                table: "Psychologists",
                column: "ScheduleId",
                principalTable: "Schedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
