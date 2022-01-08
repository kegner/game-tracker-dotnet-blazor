using GameTracker.Shared.Models;

namespace GameTracker.Server.Services
{
    public interface IGameService
    {
        Task<GameDto> GetGame(long id);
        Task<GamesDto> GetGames();
        Task<GamesDto> GetGames(long? userId);
        Task<GameDto> InsertGame(GameDto game);
        Task<GameDto> UpsertGame(GameDto game);
        Task<long> DeleteGame(long id);
    }
}