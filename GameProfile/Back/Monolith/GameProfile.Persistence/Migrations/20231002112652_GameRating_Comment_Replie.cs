using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameProfile.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class GameRating_Comment_Replie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameHasComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameHasComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameHasComments_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_GameHasComments_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GameHasRatingFromProfiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReviewScore = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameHasRatingFromProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameHasRatingFromProfiles_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameHasRatingFromProfiles_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameCommentHasReplies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Replie = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameCommentHasReplies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameCommentHasReplies_GameHasComments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "GameHasComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameCommentHasReplies_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameCommentHasReplies_CommentId",
                table: "GameCommentHasReplies",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_GameCommentHasReplies_Id",
                table: "GameCommentHasReplies",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameCommentHasReplies_ProfileId",
                table: "GameCommentHasReplies",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_GameHasComments_GameId",
                table: "GameHasComments",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameHasComments_Id",
                table: "GameHasComments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameHasComments_ProfileId",
                table: "GameHasComments",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_GameHasRatingFromProfiles_GameId",
                table: "GameHasRatingFromProfiles",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameHasRatingFromProfiles_Id",
                table: "GameHasRatingFromProfiles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GameHasRatingFromProfiles_ProfileId",
                table: "GameHasRatingFromProfiles",
                column: "ProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameCommentHasReplies");

            migrationBuilder.DropTable(
                name: "GameHasRatingFromProfiles");

            migrationBuilder.DropTable(
                name: "GameHasComments");
        }
    }
}
