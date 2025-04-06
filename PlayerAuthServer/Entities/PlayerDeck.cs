using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;
using MongoDB.Bson;

namespace PlayerAuthServer.Entities
{
    /// <summary>
    /// Represents a deck owned by a specific player.
    /// This is a many-to-one relationship: a player can have many decks,
    /// but each deck is associated with exactly one player.
    /// DeckId is stored as a string in the database, converted from MongoDB's ObjectId.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class PlayerDeck(ObjectId deckId, Guid playerId)
    {
        [JsonIgnore]
        [ForeignKey("PlayerGuid")]
        public Player? Player { get; set; }

        [Key]
        public ObjectId DeckId { get; set; } = deckId;

        [Key]
        public Guid PlayerId { get; set; } = playerId;
    }
}
