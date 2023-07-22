using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameProfile.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNotSteamGameAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotGameSteamIds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SteamAppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotGameSteamIds", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotGameSteamIds_Id",
                table: "NotGameSteamIds",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NotGameSteamIds_SteamAppId",
                table: "NotGameSteamIds",
                column: "SteamAppId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotGameSteamIds");
        }
    }
}
