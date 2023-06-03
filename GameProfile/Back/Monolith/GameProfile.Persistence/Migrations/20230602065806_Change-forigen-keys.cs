using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameProfile.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Changeforigenkeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProfileHasGames_GameId",
                table: "ProfileHasGames",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfileHasGames_ProfileId",
                table: "ProfileHasGames",
                column: "ProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileHasGames_Games_GameId",
                table: "ProfileHasGames",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProfileHasGames_Profiles_ProfileId",
                table: "ProfileHasGames",
                column: "ProfileId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProfileHasGames_Games_GameId",
                table: "ProfileHasGames");

            migrationBuilder.DropForeignKey(
                name: "FK_ProfileHasGames_Profiles_ProfileId",
                table: "ProfileHasGames");

            migrationBuilder.DropIndex(
                name: "IX_ProfileHasGames_GameId",
                table: "ProfileHasGames");

            migrationBuilder.DropIndex(
                name: "IX_ProfileHasGames_ProfileId",
                table: "ProfileHasGames");
        }
    }
}
