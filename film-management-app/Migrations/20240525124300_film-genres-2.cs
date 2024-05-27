using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace film_management_app.Server.Migrations
{
    public partial class filmgenres2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmGenre_Films_FilmsId",
                table: "FilmGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmGenre_Genres_GenresId",
                table: "FilmGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilmGenre",
                table: "FilmGenre");

            migrationBuilder.RenameColumn(
                name: "GenresId",
                table: "FilmGenre",
                newName: "GenreId");

            migrationBuilder.RenameColumn(
                name: "FilmsId",
                table: "FilmGenre",
                newName: "FilmId");

            migrationBuilder.RenameIndex(
                name: "IX_FilmGenre_GenresId",
                table: "FilmGenre",
                newName: "IX_FilmGenre_GenreId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FilmGenre",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilmGenre",
                table: "FilmGenre",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_FilmGenre_FilmId",
                table: "FilmGenre",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_FilmGenre_Films_FilmId",
                table: "FilmGenre",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmGenre_Genres_GenreId",
                table: "FilmGenre",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FilmGenre_Films_FilmId",
                table: "FilmGenre");

            migrationBuilder.DropForeignKey(
                name: "FK_FilmGenre_Genres_GenreId",
                table: "FilmGenre");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FilmGenre",
                table: "FilmGenre");

            migrationBuilder.DropIndex(
                name: "IX_FilmGenre_FilmId",
                table: "FilmGenre");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FilmGenre");

            migrationBuilder.RenameColumn(
                name: "GenreId",
                table: "FilmGenre",
                newName: "GenresId");

            migrationBuilder.RenameColumn(
                name: "FilmId",
                table: "FilmGenre",
                newName: "FilmsId");

            migrationBuilder.RenameIndex(
                name: "IX_FilmGenre_GenreId",
                table: "FilmGenre",
                newName: "IX_FilmGenre_GenresId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FilmGenre",
                table: "FilmGenre",
                columns: new[] { "FilmsId", "GenresId" });

            migrationBuilder.AddForeignKey(
                name: "FK_FilmGenre_Films_FilmsId",
                table: "FilmGenre",
                column: "FilmsId",
                principalTable: "Films",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FilmGenre_Genres_GenresId",
                table: "FilmGenre",
                column: "GenresId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
