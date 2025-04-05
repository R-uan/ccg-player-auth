using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PlayerAuthServer.Database.Entities
{
    /// <summary>
    /// Serves as an association table between Player and a Deck located in another system
    /// </summary>
    public class PlayerDeck
    {
        [Key] public Guid DeckGuid { get; set; }
        [Key] public Guid PlayerGuid { get; set; }

        [JsonIgnore]
        [ForeignKey("PlayerGuid")]
        public Player? Player { get; set; }
    }
}
