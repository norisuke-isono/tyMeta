using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddDirectorAndDesk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Desk",
                table: "Specifications",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Director",
                table: "Specifications",
                maxLength: 255,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desk",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "Director",
                table: "Specifications");
        }
    }
}
