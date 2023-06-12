using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTour.DataAccess.Persistence.Migrations
{
    public partial class ResultReportedByContender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "ResultReportedByContenderId",
                table: "Matches",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "16d9d95f-6287-49dc-a115-18bb36ba78fd", "7f08ed4c-e529-444d-9d59-bf785e6dcf86", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "21cb1fdc-bf9f-4d51-a09e-75c87a370815", "27004b9c-407d-4455-a29c-252bb347b065", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4edd4d9a-0a60-40c3-9d05-ec6d0a2311c5", "b8fa853f-2e7f-4800-ac5b-47a8ba82e086", "Contender", "CONTENDER" });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_ResultReportedByContenderId",
                table: "Matches",
                column: "ResultReportedByContenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Matches_AspNetUsers_ResultReportedByContenderId",
                table: "Matches",
                column: "ResultReportedByContenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Matches_AspNetUsers_ResultReportedByContenderId",
                table: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Matches_ResultReportedByContenderId",
                table: "Matches");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "16d9d95f-6287-49dc-a115-18bb36ba78fd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21cb1fdc-bf9f-4d51-a09e-75c87a370815");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4edd4d9a-0a60-40c3-9d05-ec6d0a2311c5");

            migrationBuilder.DropColumn(
                name: "ResultReportedByContenderId",
                table: "Matches");

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
    }
}
