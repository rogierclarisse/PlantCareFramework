using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCareFramework.Migrations
{
    public partial class WaterNeededAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WaterNeeded",
                table: "Plant",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WaterNeeded",
                table: "Plant");
        }
    }
}
