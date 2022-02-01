using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlantCareFramework.Migrations
{
    public partial class UserInStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Plant",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Plant_AppUserId",
                table: "Plant",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Plant_AspNetUsers_AppUserId",
                table: "Plant",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Plant_AspNetUsers_AppUserId",
                table: "Plant");

            migrationBuilder.DropIndex(
                name: "IX_Plant_AppUserId",
                table: "Plant");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Plant");
        }
    }
}
