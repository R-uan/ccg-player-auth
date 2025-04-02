namespace PlayerAuthServer.Utilities.Exceptions
{
    public class DuplicateEmailException(string message) : Exception(message)
    {
    }
}