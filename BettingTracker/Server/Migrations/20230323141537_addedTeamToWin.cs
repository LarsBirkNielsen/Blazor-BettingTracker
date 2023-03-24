using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BettingTracker.Server.Migrations
{
    public partial class addedTeamToWin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TeamToWin",
                table: "Predictions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Predictions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "KickOff", "TeamToWin" },
                values: new object[] { new DateTime(2023, 3, 23, 15, 15, 37, 388, DateTimeKind.Local).AddTicks(9792), "Napoli" });

            migrationBuilder.UpdateData(
                table: "Predictions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "KickOff", "TeamToWin" },
                values: new object[] { new DateTime(2023, 3, 23, 15, 15, 37, 388, DateTimeKind.Local).AddTicks(9799), "Manchester City" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TeamToWin",
                table: "Predictions");

            migrationBuilder.UpdateData(
                table: "Predictions",
                keyColumn: "Id",
                keyValue: 1,
                column: "KickOff",
                value: new DateTime(2023, 3, 23, 14, 10, 22, 116, DateTimeKind.Local).AddTicks(5077));

            migrationBuilder.UpdateData(
                table: "Predictions",
                keyColumn: "Id",
                keyValue: 2,
                column: "KickOff",
                value: new DateTime(2023, 3, 23, 14, 10, 22, 116, DateTimeKind.Local).AddTicks(5084));
        }
    }
}
