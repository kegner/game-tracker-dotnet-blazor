namespace GameTracker.Client.Store.Actions
{
    public abstract class SetSavingAction
    {
        public bool IsSaving { get; } = false;

        protected SetSavingAction(bool isSaving)
        {
            IsSaving = isSaving;
        }
    }
}
