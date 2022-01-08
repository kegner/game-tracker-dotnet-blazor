using GameTracker.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameTracker.Server.Controllers
{
    public interface IGameController
    {
        Task<ActionResult<GameDto>> GetGame(long id);
        Task<ActionResult<GamesDto>> GetGames(long? userId = null);
        Task<ActionResult<GameDto>> InsertGame(GameDto game);
        Task<ActionResult<GameDto>> UpsertGame(GameDto game);
        Task<ActionResult<long>> DeleteGame(long id);
    }
}