using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public class InMemGamesRepository : IGamesReository
{
    private readonly List<Game> games = new()
    {
        new Game(){
            Id=1,
            Name="Street Fighter II",
            Genre="Fighting",
            Price=19.99m,
            ReleaseDate = new DateTime(1991,1,1),
            ImageUri="https://www.placehold.co/150"
        },
        new Game(){
            Id=2,
            Name="Final Fantasy XIV",
            Genre="Roleplaying",
            Price=59.99m,
            ReleaseDate = new DateTime(2010,9,30),
            ImageUri="https://www.placehold.co/150"
        },
        new Game(){
            Id=3,
            Name="Fifa 2023",
            Genre="Sports",
            Price=69.99m,
            ReleaseDate = new DateTime(2022,9,27),
            ImageUri="https://www.placehold.co/150"
        }
    };
    public IEnumerable<Game> GetAll()
    {
        return games;
    }
    public Game? Get(int id)
    {
        return games.Find(game => game.Id == id);
    }

    public void Create(Game game)
    {
        if (games.Count == 0)
        {
            game.Id = 1;
        }
        else
        {
            game.Id = games.Max(game => game.Id) + 1;
        }
        games.Add(game);
    }

    public void Update(Game updatedGame)
    {
        var index = games.FindIndex(game => updatedGame.Id == game.Id);
        games[index] = updatedGame;
    }

    public void Delete(int id)
    {
        var index = games.FindIndex(game => id == game.Id);
        games.RemoveAt(index);
    }
}