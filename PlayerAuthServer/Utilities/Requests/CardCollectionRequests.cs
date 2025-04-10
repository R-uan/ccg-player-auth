namespace PlayerAuthServer.Utilities.Requests
{
    public class CollectCardRequest
    {
        public required Guid CardId { get; set; }
        public required int Amount { get; set; }

        public CollectCardRequest() { }
        public CollectCardRequest(Guid cardId, int amount)
        {
            this.CardId = cardId;
            this.Amount = amount;
        }
    }
}
