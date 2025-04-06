using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace PlayerAuthServer.Utilities.Requests
{
    public class LinkDeckRequest
    {
        [Required]
        [BsonRepresentation(BsonType.ObjectId)]
        public required ObjectId DeckId { get; set; }
    }
}
