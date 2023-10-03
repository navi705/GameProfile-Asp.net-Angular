using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameProfile.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GameHasReplie_AddProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "GameCommentHasReplies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "GameCommentHasReplies");
        }
    }
}
