namespace GameTracker.Client.Store.Actions
{
    public class SetShowSuccessToastAction : SetShowToastAction
    {
        public SetShowSuccessToastAction(string message) : base(false, message) { }
    }
}
