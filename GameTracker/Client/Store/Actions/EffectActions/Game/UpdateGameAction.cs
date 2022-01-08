using GameTracker.Shared.Models;

namespace GameTracker.Client.Store.Actions
{
    public class UpdateGameAction
    {
        public GameViewModel Game { get; }

        public UpdateGameAction(GameViewModel game)
        {
            Game = game;
        }
    }
}
