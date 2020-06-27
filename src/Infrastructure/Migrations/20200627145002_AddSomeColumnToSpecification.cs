using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddSomeColumnToSpecification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ArticleSourceNote",
                table: "Specifications",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Keyword",
                table: "Specifications",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tag",
                table: "Specifications",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VideoSourceNote",
                table: "Specifications",
                maxLength: 1000,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleSourceNote",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "Keyword",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "Tag",
                table: "Specifications");

            migrationBuilder.DropColumn(
                name: "VideoSourceNote",
                table: "Specifications");
        }
    }
}
