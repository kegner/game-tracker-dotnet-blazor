using GameTracker.Shared.Models;

namespace GameTracker.Client.Store.Actions
{
    public class FetchGamesAction
    {
        public long Id { get; }

        public FetchGamesAction(long id)
        {
            Id = id;
        }
    }
}
