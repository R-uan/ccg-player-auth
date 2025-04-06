using PlayerAuthServer.Entities;
using PlayerAuthServer.Utilities;
using Microsoft.EntityFrameworkCore;

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
                player.Property(p => p.Id)
                    .HasDefaultValueSql("gen_random_Id()");
                player.HasKey(p => p.Id);
                player.HasMany(p => p.Decks)
                    .WithOne(d => d.Player)
                    .HasForeignKey(d => d.PlayerId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<PlayerDeck>(decks =>
            {
                decks.ToTable("player_decks");
                decks.HasKey(deck => new { deck.PlayerId, deck.DeckId });
                decks.Property(deck => deck.DeckId).HasConversion(new ObjectIdToStringConverter());
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}