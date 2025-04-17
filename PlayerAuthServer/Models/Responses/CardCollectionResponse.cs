using PlayerAuthServer.Models;

namespace PlayerAuthServer
{
    public record CheckCollectionResponse(List<CardCollection> OwnedCards, List<Guid> UnownedCards, List<string> InvalidCards);
}