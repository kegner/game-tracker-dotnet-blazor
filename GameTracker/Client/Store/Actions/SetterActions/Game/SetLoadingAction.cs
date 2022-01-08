namespace GameTracker.Client.Store.Actions
{
    public abstract class SetLoadingAction
    {
        public bool IsLoading { get; } = false;

        protected SetLoadingAction(bool isLoading)
        {
            IsLoading = isLoading;
        }
    }
}
