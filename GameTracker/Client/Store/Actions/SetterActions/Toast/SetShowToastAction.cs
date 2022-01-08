namespace GameTracker.Client.Store.Actions
{
    public abstract class SetShowToastAction
    {
        public bool IsError { get; } = false;
        public string Message { get; } = "";

        protected SetShowToastAction(bool isError, string message)
        {
            IsError = isError;
            Message = message;
        }
    }
}
