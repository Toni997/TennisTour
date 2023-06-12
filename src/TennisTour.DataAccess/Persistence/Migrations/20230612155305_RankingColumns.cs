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
                keyValue: "11d92756-cdc9-45f2-bd53-46a6546281e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7ca2c6e3-adc4-4336-9001-1f8476d6c670");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8bd80ecd-f5ef-45ac-855d-be73952e478e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "223f27ac-9c93-44ba-9f5f-1dc2de3a16f2", "0fbe0c91-7d25-424d-b7f5-e4e9de3013a0", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8e01de3c-6ac9-4c3a-8d9f-c04c102c75bd", "b96e8222-8e37-415d-806d-42efc283e4b1", "Contender", "CONTENDER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ae482458-5f0b-438a-ab5a-ef54d264449a", "257eeed9-3730-450e-9b19-a9f8db0b12c6", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "223f27ac-9c93-44ba-9f5f-1dc2de3a16f2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8e01de3c-6ac9-4c3a-8d9f-c04c102c75bd");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ae482458-5f0b-438a-ab5a-ef54d264449a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "11d92756-cdc9-45f2-bd53-46a6546281e4", "6cc0c2f4-8f56-4313-8c41-9ba483804812", "Contender", "CONTENDER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7ca2c6e3-adc4-4336-9001-1f8476d6c670", "1ff5515b-4e88-4fbc-9f38-e6a9f7805a41", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8bd80ecd-f5ef-45ac-855d-be73952e478e", "a4e87fe4-4efb-4166-977a-7166358a2ea0", "User", "USER" });
        }
    }
}
