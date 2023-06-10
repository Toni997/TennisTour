using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTour.DataAccess.Persistence.Migrations
{
    public partial class RankingColumns : Migration
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
                name: "CreatedBy",
                table: "Rankings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Rankings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Rankings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Rankings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1fb5fb07-d40c-4494-9e78-2063dea86e5b", "1ed8c6bf-a6c7-47f9-8976-3da235a6b4b9", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "73335c1a-11e7-4f42-b65c-5fe69cf38c91", "455f35a3-468f-440d-9998-840d3e5cb76c", "Contender", "CONTENDER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d18f2e58-097b-44e7-bce3-6c1074541b9f", "be5f94d2-9de2-4433-b8f8-5cea1d5a0f6f", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1fb5fb07-d40c-4494-9e78-2063dea86e5b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73335c1a-11e7-4f42-b65c-5fe69cf38c91");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d18f2e58-097b-44e7-bce3-6c1074541b9f");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Rankings");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Rankings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Rankings");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Rankings");

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
