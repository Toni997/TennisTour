using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTour.DataAccess.Persistence.Migrations
{
    public partial class RemoveNextMatchIdAddControlNumberInstead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_Matches_NextMatchId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_NextMatchId",
                table: "Matches");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1dd01c7b-b22d-456a-95de-1e6a68b94101");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "365c3618-7f52-4c15-aeb6-c8e29169470a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3cdd9dd1-c3dc-4a66-8462-812be5614bac");

            migrationBuilder.DropColumn(
                name: "NextMatchId",
                table: "Matches");

            migrationBuilder.AddColumn<int>(
                name: "NextMatchupControlNumber",
                table: "Matches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "281f545f-c3a9-4090-abeb-02a0e45ce730", "70684209-035f-43ee-92ec-568ee2e2c858", "Contender", "CONTENDER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cd9da8aa-73f6-42de-b836-1da13a3fb92d", "46e07b16-2c40-4651-9ca0-f5e1cda2bb55", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ed53272b-1c75-4f83-ba18-2f0e5b7bce0d", "798a95c7-ab95-459a-a2ba-3037d3b4a1e4", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "281f545f-c3a9-4090-abeb-02a0e45ce730");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cd9da8aa-73f6-42de-b836-1da13a3fb92d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed53272b-1c75-4f83-ba18-2f0e5b7bce0d");

            migrationBuilder.DropColumn(
                name: "NextMatchupControlNumber",
                table: "Matches");

            migrationBuilder.AddColumn<Guid>(
                name: "NextMatchId",
                table: "Matches",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1dd01c7b-b22d-456a-95de-1e6a68b94101", "7899b6aa-fe19-4b1b-a732-beec1b2a719a", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "365c3618-7f52-4c15-aeb6-c8e29169470a", "02db18b9-5b1a-44c1-b24f-ed91ed8b0eda", "Contender", "CONTENDER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3cdd9dd1-c3dc-4a66-8462-812be5614bac", "46d37f04-5659-4e54-9952-1232984b99a8", "Admin", "ADMIN" });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_NextMatchId",
                table: "Matches",
                column: "NextMatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_Matches_NextMatchId",
                table: "Matches",
                column: "NextMatchId",
                principalTable: "Matches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
