using PlayerAuthServer.Models;
using PlayerAuthServer.Interfaces;
using PlayerAuthServer.Utilities.Requests;
using PlayerAuthServer.Core.Services;
using PlayerAuthServer.Utilities.Exceptions;
using PlayerAuthServer.Database.Repositories;

namespace PlayerAuthServer.Services
{
    public class CardCollectionService(ICardCollectionRepository cardRepository, IPlayerRepository playerRepository) : ICardCollectionService
    {
        public async Task<CardCollection> CollectCard(CollectCardRequest request, Guid playerId)
        {
            var newAcquisition = new CardCollection(request.CardId, request.Amount, playerId);
            var saveAcquisiton = await cardRepository.SaveEntity(newAcquisition);
            return saveAcquisiton;
        }

        public async Task<List<CardCollection>?> FindPlayerCollection(Guid playerId)
        {
            if (await playerRepository.FindPlayer(playerId) == null) return null;
            var cardCollection = await cardRepository.FindPlayerCardCollection(playerId);
            return cardCollection;
        }
    }
}