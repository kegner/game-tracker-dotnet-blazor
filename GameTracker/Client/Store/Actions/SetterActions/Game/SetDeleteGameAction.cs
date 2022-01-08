namespace GameTracker.Client.Store.Actions
{
    public class SetDeleteGameAction
    {
        public long Id { get; }

        public SetDeleteGameAction(long id)
        {
            Id = id;
        }
    }
}
