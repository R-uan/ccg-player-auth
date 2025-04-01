namespace PlayerAuthServer.Exceptions
{
    public class DuplicateEmailException(string message) : Exception(message)
    {
    }
}