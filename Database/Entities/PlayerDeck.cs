using System.ComponentModel.DataAnnotations;

namespace PlayerAuthServer.Database.Entities
{
    public class PlayerDeck
    {
        [Key] public Guid DeckGuid { get; set; }
        [Key] public Guid PlayerGuid { get; set; }
    }
}
