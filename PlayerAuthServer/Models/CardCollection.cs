using PlayerAuthServer.Entities;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PlayerAuthServer.Models
{
    public class CardCollection(Guid cardId, int amount, Guid playerId)
    {
        [Key]
        public Guid CardId { get; set; } = cardId;

        [Key]
        public Guid PlayerId { get; set; } = playerId;
        public int Amount { get; set; } = amount;

        [JsonIgnore]
        [ForeignKey("PlayerGuid")]
        public Player? Player { get; set; }
    }
}