using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class OwnerIsChangedAsAUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_AspNetUsers_OwnerId",
                table: "Objectives");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Objectives",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Objectives_OwnerId",
                table: "Objectives",
                newName: "IX_Objectives_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_AspNetUsers_UserId",
                table: "Objectives",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_AspNetUsers_UserId",
                table: "Objectives");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Objectives",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Objectives_UserId",
                table: "Objectives",
                newName: "IX_Objectives_OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_AspNetUsers_OwnerId",
                table: "Objectives",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
