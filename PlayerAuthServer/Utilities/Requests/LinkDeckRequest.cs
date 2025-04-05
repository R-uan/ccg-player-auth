using System;

namespace PlayerAuthServer.Utilities.Requests
{
    public class LinkDeckRequest
    {
        public required Guid DeckUUID { get; set; }
    }
}
