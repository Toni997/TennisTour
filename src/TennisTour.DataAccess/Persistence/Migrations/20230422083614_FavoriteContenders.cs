using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTour.DataAccess.Persistence.Migrations
{
    public partial class FavoriteContenders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFavorite",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FavoriteUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFavorite", x => new { x.UserId, x.FavoriteUserId });
                    table.ForeignKey(
                        name: "FK_UserFavorite_AspNetUsers_FavoriteUserId",
                        column: x => x.FavoriteUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFavorite_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFavorite_FavoriteUserId",
                table: "UserFavorite",
                column: "FavoriteUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFavorite");
        }
    }
}
