using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TvPrograms",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateUserId = table.Column<string>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TvPrograms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Corners",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateUserId = table.Column<string>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<string>(maxLength: 50, nullable: true),
                    TvProgramId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Corners_TvPrograms_TvProgramId",
                        column: x => x.TvProgramId,
                        principalTable: "TvPrograms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseSchedules",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateUserId = table.Column<string>(nullable: true),
                    UpdateUserId = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    Sequence = table.Column<int>(nullable: false),
                    CornerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseSchedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseSchedules_Corners_CornerId",
                        column: x => x.CornerId,
                        principalTable: "Corners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseSchedules_CornerId",
                table: "BaseSchedules",
                column: "CornerId");

            migrationBuilder.CreateIndex(
                name: "IX_Corners_TvProgramId",
                table: "Corners",
                column: "TvProgramId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseSchedules");

            migrationBuilder.DropTable(
                name: "Corners");

            migrationBuilder.DropTable(
                name: "TvPrograms");
        }
    }
}
