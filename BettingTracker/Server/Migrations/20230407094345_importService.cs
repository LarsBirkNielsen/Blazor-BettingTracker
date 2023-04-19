using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BettingTracker.Server.Migrations
{
    public partial class importService : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCurrentInLeague",
                table: "Teams",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCurrentInLeague",
                table: "Teams");
        }
    }
}
