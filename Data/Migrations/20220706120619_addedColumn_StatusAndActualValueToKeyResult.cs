using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class addedColumn_StatusAndActualValueToKeyResult : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "ActualValue",
                table: "KeyResults",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "KeyResults",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActualValue",
                table: "KeyResults");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "KeyResults");
        }
    }
}
