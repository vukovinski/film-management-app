using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace film_management_app.Server.Migrations
{
    public partial class acceptedrole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AcceptedRole",
                table: "FilmStars",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedRole",
                table: "FilmStars");
        }
    }
}
