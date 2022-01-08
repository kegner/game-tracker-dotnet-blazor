namespace GameTracker.Client.Store.Actions
{
    public class DeleteGameAction
    {
        public long Id { get; }

        public DeleteGameAction(long id)
        {
            Id = id;
        }
    }
}
