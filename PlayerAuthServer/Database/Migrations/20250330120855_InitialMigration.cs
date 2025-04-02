using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlayerAuthServer.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "players",
                columns: table => new
                {
                    UUID = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Nickname = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_players", x => x.UUID);
                });

            migrationBuilder.CreateTable(
                name: "player_decks",
                columns: table => new
                {
                    DeckGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerGuid = table.Column<Guid>(type: "uuid", nullable: false),
                    PlayerUUID = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_player_decks", x => new { x.PlayerGuid, x.DeckGuid });
                    table.ForeignKey(
                        name: "FK_player_decks_players_PlayerUUID",
                        column: x => x.PlayerUUID,
                        principalTable: "players",
                        principalColumn: "UUID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_player_decks_PlayerUUID",
                table: "player_decks",
                column: "PlayerUUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "player_decks");

            migrationBuilder.DropTable(
                name: "players");
        }
    }
}
