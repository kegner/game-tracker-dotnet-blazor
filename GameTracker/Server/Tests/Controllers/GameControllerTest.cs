using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using GameTracker.Server.Controllers;
using GameTracker.Server.Services;
using GameTracker.Shared.Models;
using Moq;

namespace GameTracker.Server.Tests.Controllers
{
    [TestFixture]
    public class GameControllerTest
    {
        private GameController controller;
        private Mock<IGameService> serviceMock;

        [SetUp]
        public void SetUp()
        {
            GameDto game = new()
            {
                Id = 1
            };
            List<GameDto> gamesList = new()
            {
                game
            };
            GamesDto games = new(gamesList);

            serviceMock = new Mock<IGameService>();
            serviceMock.Setup(s => s.GetGame(It.IsAny<long>())).Returns(Task.FromResult(game));
            serviceMock.Setup(s => s.GetGames()).Returns(Task.FromResult(games));
            serviceMock.Setup(s => s.GetGames(It.IsAny<long>())).Returns(Task.FromResult(games));
            serviceMock.Setup(s => s.InsertGame(It.IsAny<GameDto>())).Returns(Task.FromResult(game));
            serviceMock.Setup(s => s.UpsertGame(It.IsAny<GameDto>())).Returns(Task.FromResult(game));
            serviceMock.Setup(s => s.DeleteGame(It.IsAny<long>())).Returns(Task.FromResult(1L));
            controller = new(serviceMock.Object);
        }

        [Test]
        public async Task GetGameTest()
        {
            ActionResult<GameDto> result = await controller.GetGame(1L);
            Assert.AreEqual(1L, result.Value.Id);
        }

        [Test]
        public async Task GetGamesTest()
        {
            ActionResult<GamesDto> result = await controller.GetGames(1L);
            Assert.AreEqual(1L, result.Value.GamesList[0].Id);
        }

        [Test]
        public async Task InsertGameTest()
        {
            ActionResult<GameDto> result = await controller.InsertGame(new GameDto());
            Assert.AreEqual(1L, result.Value.Id);
        }

        [Test]
        public async Task UpsertGameTest()
        {
            ActionResult<GameDto> result = await controller.UpsertGame(new GameDto());
            Assert.AreEqual(1L, result.Value.Id);
        }

        [Test]
        public async Task DeleteGameTest()
        {
            ActionResult<long> result = await controller.DeleteGame(1L);
            Assert.AreEqual(1L, result.Value);
        }
    }
}
