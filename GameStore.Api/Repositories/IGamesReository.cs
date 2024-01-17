using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public interface IGamesReository
{
    void Create(Game game);
    void Delete(int id);
    Game? Get(int id);
    IEnumerable<Game> GetAll();
    void Update(Game updatedGame);
}
