namespace GameTracker.Client.Store.Actions
{
    public class SetHideToastAction
    {
        public string Id { get; }

        public SetHideToastAction(string id)
        {
            Id = id;
        }
    }
}
