using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameProfile.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class PostHaveRatingFromProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostHaveRatingFromProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PostId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsPositive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostHaveRatingFromProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostHaveRatingFromProfiles_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostHaveRatingFromProfiles_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostHaveRatingFromProfiles_Id",
                table: "PostHaveRatingFromProfiles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostHaveRatingFromProfiles_PostId",
                table: "PostHaveRatingFromProfiles",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostHaveRatingFromProfiles_ProfileId",
                table: "PostHaveRatingFromProfiles",
                column: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostHaveRatingFromProfiles");
        }
    }
}
