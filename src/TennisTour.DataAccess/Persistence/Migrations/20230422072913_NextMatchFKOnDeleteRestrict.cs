using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTour.DataAccess.Persistence.Migrations
{
    public partial class NextMatchFKOnDeleteRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Matches_NextMatchId",
                table: "Matches");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Matches_NextMatchId",
                table: "Matches",
                column: "NextMatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Matches_NextMatchId",
                table: "Matches");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Matches_NextMatchId",
                table: "Matches",
                column: "NextMatchId",
                principalTable: "Matches",
                principalColumn: "Id");
        }
    }
}
