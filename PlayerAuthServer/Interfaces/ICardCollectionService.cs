using PlayerAuthServer.Models;
using PlayerAuthServer.Utilities.Requests;

namespace PlayerAuthServer.Interfaces
{
    public interface ICardCollectionService
    {
        Task<CardCollection> CollectCard(CollectCardRequest request, Guid playerId);
    }
}
