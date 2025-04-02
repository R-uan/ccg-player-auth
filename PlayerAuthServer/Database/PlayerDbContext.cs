using Microsoft.EntityFrameworkCore;
using PlayerAuthServer.Database.Entities;

namespace PlayerAuthServer.Database
{
    public class PlayerDbContext(DbContextOptions<PlayerDbContext> options) : DbContext(options)
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<PlayerDeck> PlayerDecks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>(player =>
            {
                player.ToTable("players");
                player.Property(p => p.UUID)
                    .HasDefaultValueSql("gen_random_uuid()");
                player.HasKey(p => p.UUID);
            });

            modelBuilder.Entity<PlayerDeck>(decks =>
            {
                decks.ToTable("player_decks");
                decks.HasKey(deck => new { deck.PlayerGuid, deck.DeckGuid });
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}