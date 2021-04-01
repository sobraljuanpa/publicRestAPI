using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IDataAccess.Migrations
{
    public partial class FullDomainMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consultations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientBirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientPhone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Consultations");
        }
    }
}
