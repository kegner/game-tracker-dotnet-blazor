using GameTracker.Server.Services;
using GameTracker.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameTracker.Server.Controllers
{
    [ApiController]
    [Route("api/v1/games")]
    [Authorize]
    public class GameController : ControllerBase, IGameController
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGame(long id)
        {
            return await _gameService.GetGame(id);
        }

        [HttpGet]
        public async Task<ActionResult<GamesDto>> GetGames(long? userId = null)
        {
            return await _gameService.GetGames(userId);
        }

        [HttpPost]
        public async Task<ActionResult<GameDto>> InsertGame(GameDto game)
        {
            return await _gameService.InsertGame(game);
        }

        [HttpPut]
        public async Task<ActionResult<GameDto>> UpsertGame(GameDto game)
        {
            return await _gameService.UpsertGame(game);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<long>> DeleteGame(long id)
        {
            return await _gameService.DeleteGame(id);
        }
    }
}
