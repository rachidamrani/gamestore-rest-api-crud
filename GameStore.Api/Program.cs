using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IGamesReository, InMemGamesRepository>();
builder.Configuration.GetConnectionString("GameStoreContext");
builder.Services.AddDbContext<GameStoreContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("GameStoreContext")));

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();