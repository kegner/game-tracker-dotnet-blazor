using GameTracker.Shared.Models;

namespace GameTracker.Client.Store.Actions
{
    public class SetFetchGamesAction
    {
        public List<GameDto> Games { get; }

        public SetFetchGamesAction(List<GameDto> games)
        {
            Games = games;
        }
    }
}
