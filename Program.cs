using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PlayerAuthServer.Database;
using PlayerAuthServer.Database.Repositories;
using PlayerAuthServer.Interfaces;
using PlayerAuthServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PlayerAuthServer.Utilities;
using System.Text;
using Microsoft.Extensions.Options;

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
builder.Services.AddScoped<JwtService>();

builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();
builder.Services.AddScoped<IPlayerService, PlayerService>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var jwtSettings = jwtSettingsSection.Get<JwtSettings>()
        ?? throw new Exception("JwtSettings were not found on the configuration file.");
    var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SigningKey));
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        IssuerSigningKey = signingKey
    };
});


var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
