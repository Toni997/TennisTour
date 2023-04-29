using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTour.DataAccess.Persistence.Migrations
{
    public partial class SeedRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a8c1cd57-cc8b-47f1-a735-7e26e841fb5c", "9fd6b3dc-178f-4c23-af78-65b074da0a05", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff99aba7-a64e-4a42-8eca-eb0f07af97f2", "838bd940-29f5-4194-a96c-dd5553a3af12", "Contender", "CONTENDER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ff9e2cff-a6c0-46f0-bf3e-b1809cbe18b2", "c7304164-f973-4bf5-8925-be24235118e4", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a8c1cd57-cc8b-47f1-a735-7e26e841fb5c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff99aba7-a64e-4a42-8eca-eb0f07af97f2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ff9e2cff-a6c0-46f0-bf3e-b1809cbe18b2");
        }
    }
}
