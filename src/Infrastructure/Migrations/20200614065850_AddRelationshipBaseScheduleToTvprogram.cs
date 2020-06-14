using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class AddRelationshipBaseScheduleToTvprogram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseSchedules_Corners_CornerId",
                table: "BaseSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "CornerId",
                table: "BaseSchedules",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TvProgramId",
                table: "BaseSchedules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BaseSchedules_TvProgramId_Sequence",
                table: "BaseSchedules",
                columns: new[] { "TvProgramId", "Sequence" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseSchedules_Corners_CornerId",
                table: "BaseSchedules",
                column: "CornerId",
                principalTable: "Corners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseSchedules_TvPrograms_TvProgramId",
                table: "BaseSchedules",
                column: "TvProgramId",
                principalTable: "TvPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseSchedules_Corners_CornerId",
                table: "BaseSchedules");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseSchedules_TvPrograms_TvProgramId",
                table: "BaseSchedules");

            migrationBuilder.DropIndex(
                name: "IX_BaseSchedules_TvProgramId_Sequence",
                table: "BaseSchedules");

            migrationBuilder.DropColumn(
                name: "TvProgramId",
                table: "BaseSchedules");

            migrationBuilder.AlterColumn<int>(
                name: "CornerId",
                table: "BaseSchedules",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseSchedules_Corners_CornerId",
                table: "BaseSchedules",
                column: "CornerId",
                principalTable: "Corners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
