using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using PlayerAuthServer.Tests.E2E;
using PlayerAuthServer.Utilities.Requests;
using PlayerAuthServer.Utilities.Responses;

namespace PlayerAuthServer.Tests.E2ETests;

public class PlayerDeckApiTests(CustomWebApplicationFactory factory) : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient client = factory.CreateClient();

    [Fact]
    public async Task Tries()
    {
        var loginRequest = await client.PostAsJsonAsync<LoginRequest>("/api/auth/login", new LoginRequest
        {
            Email = "tester1@protonmail.com",
            Password = "Tester1"
        });

        loginRequest.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        var loginResult = await loginRequest.Content.ReadFromJsonAsync<LoginResponse>();

        Assert.NotNull(loginResult);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);
        var deckLinkRequest = await client.PostAsJsonAsync("/api/deck", new LinkDeckRequest { DeckUUID = Guid.NewGuid() });
        deckLinkRequest.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
    }
}
