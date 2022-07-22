using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class objectiveModelEdited : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Departments_DepartmentId",
                table: "Objectives");

            migrationBuilder.DropIndex(
                name: "IX_Objectives_DepartmentId",
                table: "Objectives");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Objectives");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Objectives",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Objectives_DepartmentId",
                table: "Objectives",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Departments_DepartmentId",
                table: "Objectives",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}
