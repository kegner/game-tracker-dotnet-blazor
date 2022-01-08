using AutoMapper;
using GameTracker.Server.Contexts;
using GameTracker.Server.Exceptions;
using GameTracker.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace GameTracker.Server.Services
{
    public class GameService : IGameService
    {
        private readonly IMapper _mapper;
        private readonly GameContext _context;

        public GameService(IMapper mapper, GameContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GameDto> GetGame(long id)
        {
            Game? game = await _context.FindAsync<Game>(id);
            return _mapper.Map<GameDto>(game);
        }

        public async Task<GamesDto> GetGames(long? userId)
        {
            Games games;
            if (userId != null)
            {
                games = new Games(await _context.Games.Where(g => g.UserId == userId).ToListAsync());
            }
            else
            {
                games = new Games(await _context.Games.ToListAsync());
            }

            return _mapper.Map<GamesDto>(games);
        }

        public async Task<GamesDto> GetGames()
        {
            return await GetGames(null);
        }

        public async Task<GameDto> InsertGame(GameDto game)
        {
            // enforce that POST should create resources
            if (game.Id != null && await _context.FindAsync<Game>(game.Id) != null)
            {
                throw new ResourceExistsException("A game with this ID already exists.");
            }

            return await Add(game);
            
        }

        public async Task<GameDto> UpsertGame(GameDto game)
        {
            if (game.Id != null && await _context.FindAsync<Game>(game.Id) != null)
            {
                return await Update(game);
            }

            return await Add(game);
        }

        public async Task<long> DeleteGame(long id)
        {
            Game existingEntry = await _context.Games.SingleAsync(g => g.Id == id);
            _context.Remove(existingEntry);
            await _context.SaveChangesAsync();
            return id;
        }

        private async Task CheckGameTitle(GameDto game)
        {
            Game? existingGame = await _context.Games.SingleOrDefaultAsync(g => g.UserId == game.UserId && g.Title == game.Title);
            if (existingGame != null && existingGame.Id != game.Id)
            {
                throw new ResourceExistsException("A game with this title already exists.");
            }
        }

        private async Task<GameDto> Add(GameDto game)
        {
            await CheckGameTitle(game);
            Game gameEntity = _mapper.Map<Game>(game);
            if (gameEntity.StartDate != null)
            {
                gameEntity.StartDate = DateTime.SpecifyKind((DateTime) gameEntity.StartDate, DateTimeKind.Utc);
            }
            if (gameEntity.EndDate != null)
            {
                gameEntity.EndDate = DateTime.SpecifyKind((DateTime)gameEntity.EndDate, DateTimeKind.Utc);
            }
            _context.Add(gameEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<GameDto>(gameEntity);
        }

        private async Task<GameDto> Update(GameDto game)
        {
            await CheckGameTitle(game);
            Game gameEntity = _mapper.Map<Game>(game);
            if (gameEntity.StartDate != null)
            {
                gameEntity.StartDate = DateTime.SpecifyKind((DateTime)gameEntity.StartDate, DateTimeKind.Utc);
            }
            if (gameEntity.EndDate != null)
            {
                gameEntity.EndDate = DateTime.SpecifyKind((DateTime)gameEntity.EndDate, DateTimeKind.Utc);
            }
            Game existingEntry = _context.Games.Single(g => g.Id == gameEntity.Id);
            _context.Entry(existingEntry).CurrentValues.SetValues(gameEntity);
            await _context.SaveChangesAsync();
            return _mapper.Map<GameDto>(gameEntity);
        }
    }
}
