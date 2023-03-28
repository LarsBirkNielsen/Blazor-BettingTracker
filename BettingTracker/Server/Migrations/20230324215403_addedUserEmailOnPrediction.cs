using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BettingTracker.Server.Migrations
{
    public partial class addedUserEmailOnPrediction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Predictions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Predictions");
        }
    }
}
