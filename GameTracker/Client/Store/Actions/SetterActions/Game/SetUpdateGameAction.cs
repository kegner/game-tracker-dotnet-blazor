using GameTracker.Shared.Models;

namespace GameTracker.Client.Store.Actions
{
    public class SetUpdateGameAction
    {
        public GameDto Game { get; }

        public SetUpdateGameAction(GameDto game)
        {
            Game = game;
        }
    }
}
