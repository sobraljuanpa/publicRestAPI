using Microsoft.EntityFrameworkCore.Migrations;

namespace IDataAccess.Migrations
{
    public partial class CategoryElementUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayableContents_Categories_CategoryId",
                table: "PlayableContents");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Categories_CategoryId",
                table: "Playlists");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Playlists",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "PlayableContents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PlayableContents_Categories_CategoryId",
                table: "PlayableContents",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Categories_CategoryId",
                table: "Playlists",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlayableContents_Categories_CategoryId",
                table: "PlayableContents");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Categories_CategoryId",
                table: "Playlists");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Playlists",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "PlayableContents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PlayableContents_Categories_CategoryId",
                table: "PlayableContents",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Categories_CategoryId",
                table: "Playlists",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
