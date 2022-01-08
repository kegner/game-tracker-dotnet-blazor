namespace GameTracker.Client.Store.Actions
{
    public class SetShowErrorToastAction : SetShowToastAction
    {
        public SetShowErrorToastAction(string message) : base(true, message) { }
    }
}
