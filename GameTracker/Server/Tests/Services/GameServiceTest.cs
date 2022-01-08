using NUnit.Framework;
using GameTracker.Server.Services;
using GameTracker.Shared.Models;
using GameTracker.Server.Contexts;
using GameTracker.Server.Configurations;
using GameTracker.Server.Exceptions;
using AutoMapper;
using Moq;
using Moq.EntityFrameworkCore;

namespace GameTracker.Server.Tests.Services
{
    [TestFixture]
    public class GameServiceTest
    {
        private GameService gameService;
        private Mock<GameContext> contextMock;
        private IMapper mapper;

        [SetUp]
        public void SetUp()
        {
            Game game = new()
            {
                Id = 1,
                Title = "Game 1",
                UserId = 1
            };

            List<Game> gameList = new()
            {
                game
            };

            contextMock = new();
            contextMock.Setup(x => x.Games).ReturnsDbSet(gameList);
            contextMock.Setup(c => c.FindAsync<Game>(It.IsAny<long>())).Returns(ValueTask.FromResult(game));

            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = new Mapper(config);

            gameService = new GameService(mapper, contextMock.Object);
        }

        [Test]
        public async Task GetGameTest()
        {
            GameDto result = await gameService.GetGame(1L);
            Assert.AreEqual(1L, result.Id);
        }

        [Test]
        public void GetGameErrorTest()
        {
            contextMock.Setup(c => c.FindAsync<Game>(It.IsAny<long>())).Throws<ResourceNotFoundException>();
            Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
            {
                await gameService.GetGame(1L);
            });
        }

        [Test]
        public async Task GetAllGamesTest()
        {
            GamesDto result = await gameService.GetGames();
            Assert.AreEqual(1L, result.GamesList[0].Id);
        }

        [Test]
        public async Task GetGamesByUserIdTest()
        {
            GamesDto result = await gameService.GetGames(1L);
            Assert.AreEqual(1L, result.GamesList[0].Id);
        }

        [Test]
        public async Task InsertGameTest()
        {
            GameDto game = new()
            {
                Id = 2,
                Title = "Game 2",
                UserId = 1
            };
            contextMock.Setup(c => c.FindAsync<Game>(It.IsAny<long>())).Returns(null);
            GameDto result = await gameService.InsertGame(game);
            Assert.AreEqual(2L, result.Id);
        }

        [Test]
        public void InsertGameErrorTest()
        {
            GameDto game = new()
            {
                Id = 2,
                Title = "Game 2",
                UserId = 1
            };
            Game gameEntity = mapper.Map<Game>(game);
            contextMock.Setup(c => c.FindAsync<Game>(It.IsAny<long>())).Returns(ValueTask.FromResult(gameEntity));
            Assert.ThrowsAsync<ResourceExistsException>(async () =>
            {
                await gameService.InsertGame(game);
            });
        }

        [Test]
        public async Task UpsertGameTest()
        {
            GameDto game = new()
            {
                Id = 3,
                Title = "Game 3",
                UserId = 1
            };
            Game gameEntity = mapper.Map<Game>(game);
            contextMock.Setup(c => c.FindAsync<Game>(It.IsAny<long>())).Returns(null);
            GameDto result = await gameService.InsertGame(game);
            Assert.AreEqual(3L, result.Id);
        }

        [Test]
        public void UpsertGameErrorTest()
        {
            GameDto game = new()
            {
                Id = 3,
                Title = "Game 1",
                UserId = 1
            };
            Game gameEntity = mapper.Map<Game>(game);
            contextMock.Setup(c => c.FindAsync<Game>(It.IsAny<long>())).Returns(ValueTask.FromResult(gameEntity));
            Assert.ThrowsAsync<ResourceExistsException>(async () =>
            {
                await gameService.UpsertGame(game);
            });
        }

        [Test]
        public async Task DeleteGameTest()
        {
            long id = await gameService.DeleteGame(1L);
            Assert.AreEqual(1L, id);
        }


    }
}
