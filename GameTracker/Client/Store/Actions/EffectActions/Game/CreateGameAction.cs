using GameTracker.Shared.Models;

namespace GameTracker.Client.Store.Actions
{
    public class CreateGameAction
    {
        public GameViewModel Game { get; }

        public CreateGameAction(GameViewModel game)
        {
            Game = game;
        }
    }
}
