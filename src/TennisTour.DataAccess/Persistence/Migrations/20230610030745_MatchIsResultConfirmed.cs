using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTour.DataAccess.Persistence.Migrations
{
    public partial class MatchIsResultConfirmed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<bool>(
                name: "IsResultConfirmed",
                table: "Matches",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsResultConfirmed",
                table: "Matches");

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
        }
    }
}
