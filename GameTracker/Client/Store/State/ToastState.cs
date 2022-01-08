using GameTracker.Shared.Models;
using Fluxor;

namespace GameTracker.Client.Store.State
{
    [FeatureState]
    public record ToastState
    {
        public List<Toast> Toasts { get; init; } = new();
    }
}
