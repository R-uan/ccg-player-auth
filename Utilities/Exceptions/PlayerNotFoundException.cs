using System;

namespace PlayerAuthServer.Exceptions
{
    public class PlayerNotFoundException(string message) : Exception(message)
    {
    }
}
