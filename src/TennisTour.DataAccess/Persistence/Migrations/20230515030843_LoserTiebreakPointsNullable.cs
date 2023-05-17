using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTour.DataAccess.Persistence.Migrations
{
    public partial class LoserTiebreakPointsNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7632b424-d358-47c3-a0e0-344dabacda10");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "99ed97da-867a-44e3-b16f-e1a82b99f9e8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cfc3e4c1-ea03-4263-8f07-1a9adc499818");

            migrationBuilder.AlterColumn<int>(
                name: "LoserTiebreakPoints",
                table: "MatchSets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<int>(
                name: "LoserTiebreakPoints",
                table: "MatchSets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7632b424-d358-47c3-a0e0-344dabacda10", "357e2bf0-6c67-453a-928d-1adf635039c9", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "99ed97da-867a-44e3-b16f-e1a82b99f9e8", "2183881b-4e71-4fbe-93d8-59ad8f3582ab", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "cfc3e4c1-ea03-4263-8f07-1a9adc499818", "1ad6e284-ce6f-46dd-9baa-8eb49d0887cb", "Contender", "CONTENDER" });
        }
    }
}
