using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameProfile.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGameAppIdSteam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameSteamIds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SteamAppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSteamIds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameSteamIds_Id",
                table: "GameSteamIds",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameSteamIds_SteamAppId",
                table: "GameSteamIds",
                column: "SteamAppId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameSteamIds");
        }
    }
}
