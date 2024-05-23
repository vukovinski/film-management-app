using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace film_management_app.Server.Migrations
{
    public partial class expectedfee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ExpectedFee",
                table: "Users",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpectedFee",
                table: "Users");
        }
    }
}
