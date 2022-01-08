using GameTracker.Shared.Models;

namespace GameTracker.Client.Store.Actions
{
    public class SetSelectGameAction
    {
        public GameDto SelectedGame { get; }

        public SetSelectGameAction(GameDto selectedGame)
        {
            SelectedGame = selectedGame;
        }
    }
}
