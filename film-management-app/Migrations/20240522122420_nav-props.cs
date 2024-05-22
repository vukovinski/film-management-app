using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace film_management_app.Server.Migrations
{
    public partial class navprops : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_FilmDirectors_Users_UserId",
                table: "FilmDirectors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmStars_Users_UserId",
                table: "FilmStars",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmDirectors_Users_UserId",
                table: "FilmDirectors");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmStars_Users_UserId",
                table: "FilmStars");
        }
    }
}
