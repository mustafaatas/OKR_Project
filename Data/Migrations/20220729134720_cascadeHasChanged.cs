using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class cascadeHasChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Objectives_SurObjectiveId",
                table: "Objectives");

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Objectives_SurObjectiveId",
                table: "Objectives",
                column: "SurObjectiveId",
                principalTable: "Objectives",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Objectives_SurObjectiveId",
                table: "Objectives");

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Objectives_SurObjectiveId",
                table: "Objectives",
                column: "SurObjectiveId",
                principalTable: "Objectives",
                principalColumn: "Id");
        }
    }
}
