using System.Text;
using PlayerAuthServer.Database;
using PlayerAuthServer.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PlayerAuthServer.Core.Services;
using PlayerAuthServer.Database.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace PlayerAuthServer
{
    public class Program
    {
        private static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddControllers();

            builder.Services.AddDbContext<PlayerDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("PgDb");
                options.UseNpgsql(connectionString);
            });

            var jwtSettingsSection = builder.Configuration.GetSection("Jwt");
            builder.Services.Configure<JwtSettings>(jwtSettingsSection);
            builder.Services.AddScoped<IJwtService, JwtService>();

            builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
            builder.Services.AddScoped<IPlayerService, PlayerService>();
            builder.Services.AddScoped<IPlayerDeckRepository, PlayerDeckRepository>();
            builder.Services.AddScoped<IPlayerDeckService, PlayerDeckService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtSettings = jwtSettingsSection.Get<JwtSettings>()
                    ?? throw new Exception("JwtSettings were not found on the configuration file.");

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey));
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = false,
                    IssuerSigningKey = signingKey,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                };
            });


            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}

