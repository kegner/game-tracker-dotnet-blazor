using GameTracker.Shared.Models;

namespace GameTracker.Client.Store.Actions
{
    public class SetCreateGameAction
    {
        public GameDto Game { get; }

        public SetCreateGameAction(GameDto game)
        {
            Game = game;
        }
    }
}
