using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class FixTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationArticleSources_articleSources_ArticleSourceId",
                table: "SpecificationArticleSources");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationVideoSources_videoSources_VideoSourceId",
                table: "SpecificationVideoSources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_videoSources",
                table: "videoSources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_articleSources",
                table: "articleSources");

            migrationBuilder.RenameTable(
                name: "videoSources",
                newName: "VideoSources");

            migrationBuilder.RenameTable(
                name: "articleSources",
                newName: "ArticleSources");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VideoSources",
                table: "VideoSources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArticleSources",
                table: "ArticleSources",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationArticleSources_ArticleSources_ArticleSourceId",
                table: "SpecificationArticleSources",
                column: "ArticleSourceId",
                principalTable: "ArticleSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationVideoSources_VideoSources_VideoSourceId",
                table: "SpecificationVideoSources",
                column: "VideoSourceId",
                principalTable: "VideoSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationArticleSources_ArticleSources_ArticleSourceId",
                table: "SpecificationArticleSources");

            migrationBuilder.DropForeignKey(
                name: "FK_SpecificationVideoSources_VideoSources_VideoSourceId",
                table: "SpecificationVideoSources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VideoSources",
                table: "VideoSources");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArticleSources",
                table: "ArticleSources");

            migrationBuilder.RenameTable(
                name: "VideoSources",
                newName: "videoSources");

            migrationBuilder.RenameTable(
                name: "ArticleSources",
                newName: "articleSources");

            migrationBuilder.AddPrimaryKey(
                name: "PK_videoSources",
                table: "videoSources",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_articleSources",
                table: "articleSources",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationArticleSources_articleSources_ArticleSourceId",
                table: "SpecificationArticleSources",
                column: "ArticleSourceId",
                principalTable: "articleSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpecificationVideoSources_videoSources_VideoSourceId",
                table: "SpecificationVideoSources",
                column: "VideoSourceId",
                principalTable: "videoSources",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
