using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PlayerAuthServer.Database;
using PlayerAuthServer.Database.Repositories;
using PlayerAuthServer.Interfaces;
using PlayerAuthServer.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddDbContext<PlayerDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PgDb");
    options.UseNpgsql(connectionString);
});

builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<IPlayerRepository, PlayerRepository>();

var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
