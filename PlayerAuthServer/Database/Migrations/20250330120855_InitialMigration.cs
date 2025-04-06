using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerAuthServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "Id", nullable: false, defaultValueSql: "gen_random_Id()"),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "player_decks",
                columns: table => new
                {
                    DeckGuid = table.Column<Guid>(type: "Id", nullable: false),
                    PlayerGuid = table.Column<Guid>(type: "Id", nullable: false),
                    PlayerId = table.Column<Guid>(type: "Id", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_decks", x => new { x.PlayerGuid, x.DeckGuid });
                    table.ForeignKey(
                        name: "FK_player_decks_players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_player_decks_PlayerId",
                table: "player_decks",
                column: "PlayerId");
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "player_decks");

            migrationBuilder.DropTable(
                name: "players");
        }
    }
}
