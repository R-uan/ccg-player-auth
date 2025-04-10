using PlayerAuthServer.Models;

namespace PlayerAuthServer.Interfaces
{
    public interface ICardCollectionRepository
    {
        Task<CardCollection> SaveEntity(CardCollection entity);
        Task<List<CardCollection>> FindPlayerCardCollection(Guid playerId);
    }
}
