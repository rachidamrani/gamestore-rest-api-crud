using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameEndPointName = "GetGame";
    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/games").WithParameterValidation();

        // Get all games
        group.MapGet("/", (IGamesReository gamesRepository) =>
            gamesRepository.GetAll().Select(game => game.AsDto())
        );

        // Get game by Id
        group.MapGet(pattern: "/{id}", (IGamesReository gamesRepository, int id) =>
        {
            Game? game = gamesRepository.Get(id);

            return game is not null ? Results.Ok(game.AsDto()) : Results.NotFound();

        }).WithName(GetGameEndPointName);

        // Create a new game
        group.MapPost("/", (IGamesReository gamesRepository, CreateGameDto gameDto) =>
        {
            Game game = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate,
                ImageUri = gameDto.ImageUri
            };

            gamesRepository.Create(game);
            return Results.CreatedAtRoute(GetGameEndPointName, new { id = game.Id }, game);
        });

        // Update an existing game
        group.MapPut("/{id}", (IGamesReository gamesRepository, int id, UpdateGameDto updateGameDto) =>
        {

            Game? existingGame = gamesRepository.Get(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            existingGame.Name = updateGameDto.Name;
            existingGame.Price = updateGameDto.Price;
            existingGame.Genre = updateGameDto.Genre;
            existingGame.ReleaseDate = updateGameDto.ReleaseDate;
            existingGame.ImageUri = updateGameDto.ImageUri;

            gamesRepository.Update(existingGame);

            return Results.NoContent();

        });

        // Delete an existing game
        group.MapDelete("/{id}", (IGamesReository gamesRepository, int id) =>
        {
            Game? existingGame = gamesRepository.Get(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            gamesRepository.Delete(id);

            return Results.NoContent();
        });

        return group;
    }
}