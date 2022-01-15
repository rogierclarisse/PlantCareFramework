using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCareFramework.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Inside",
                table: "Place");

            migrationBuilder.DropColumn(
                name: "High",
                table: "Light");

            migrationBuilder.DropColumn(
                name: "Medium",
                table: "Light");

            migrationBuilder.RenameColumn(
                name: "Outside",
                table: "Place",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "None",
                table: "Light",
                newName: "LightIntensity");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Place",
                newName: "Outside");

            migrationBuilder.RenameColumn(
                name: "LightIntensity",
                table: "Light",
                newName: "None");

            migrationBuilder.AddColumn<string>(
                name: "Inside",
                table: "Place",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "High",
                table: "Light",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Medium",
                table: "Light",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
