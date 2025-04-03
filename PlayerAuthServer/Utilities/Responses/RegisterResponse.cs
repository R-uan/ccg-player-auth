using System;

namespace PlayerAuthServer.Utilities.Responses;

public class RegisterResponse
{
    public required string Email { get; set; }
    public required string Nickname { get; set; }
}
