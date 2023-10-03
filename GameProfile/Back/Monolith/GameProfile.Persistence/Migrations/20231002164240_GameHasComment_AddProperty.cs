using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameProfile.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GameHasComment_AddProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "GameHasComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "GameHasComments");
        }
    }
}
