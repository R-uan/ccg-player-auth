using System;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlayerAuthServer.Database;

namespace PlayerAuthServer.Tests.E2E
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var dbDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<PlayerDbContext>));
                if (dbDescriptor != null) services.Remove(dbDescriptor);
                services.AddDbContext<PlayerDbContext>(options => options.UseInMemoryDatabase("E2ETest"));
                using var scope = services.BuildServiceProvider().CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<PlayerDbContext>();
                this.SeedPlayerData(db);
            });

            return base.CreateHost(builder);
        }

        private void SeedPlayerData(PlayerDbContext playerDbContext)
        {
            playerDbContext.Players.AddRange(
                new Database.Entities.Player
                {
                    Email = "tester1@protonmail.com",
                    Nickname = "tester1",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Tester1")
                },
                new Database.Entities.Player
                {
                    Email = "tester2@protonmail.com",
                    Nickname = "tester2",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("Tester2")
                }
            );

            playerDbContext.SaveChanges();
        }
    }

}