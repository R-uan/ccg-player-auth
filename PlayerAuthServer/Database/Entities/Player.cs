using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerAuthServer.Database.Entities
{
    public class Player
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UUID { get; set; }
        public required string Email { get; set; }
        public List<PlayerDeck>? Decks { get; set; }
        public required string Nickname { get; set; }
        public required string PasswordHash { get; set; }
        public ICollection<PlayerDeck>? PlayerDecks { get; set; }
    }
}
