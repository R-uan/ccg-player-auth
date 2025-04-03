using System.ComponentModel.DataAnnotations;

namespace PlayerAuthServer.Database.Entities
{
    /// <summary>
    /// Serves as an association table between Player and a Deck located in another system
    /// </summary>
    public class PlayerDeck
    {
        public Player? Player { get; set; }
        [Key] public Guid DeckGuid { get; set; }
        [Key] public Guid PlayerGuid { get; set; }
    }
}
