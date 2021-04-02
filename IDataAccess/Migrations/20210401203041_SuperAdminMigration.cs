using Microsoft.EntityFrameworkCore.Migrations;

namespace IDataAccess.Migrations
{
    public partial class SuperAdminMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Administrators",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Administrators",
                columns: new[] { "Id", "Email", "Name", "Password" },
                values: new object[] { 1, "admin@admin.admin", null, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Administrators_Email",
                table: "Administrators",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Administrators_Email",
                table: "Administrators");

            migrationBuilder.DeleteData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Administrators",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
