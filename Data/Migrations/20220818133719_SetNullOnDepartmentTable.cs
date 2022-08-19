using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class SetNullOnDepartmentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_LeaderId",
                table: "Departments");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_LeaderId",
                table: "Departments",
                column: "LeaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_AspNetUsers_LeaderId",
                table: "Departments");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_AspNetUsers_LeaderId",
                table: "Departments",
                column: "LeaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
