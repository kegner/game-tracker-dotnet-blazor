using GameTracker.Shared.Models;
using Fluxor;

namespace GameTracker.Client.Store.State
{
    [FeatureState]
    public record GameState
    {
        public List<GameDto> Games { get; init; } = new();
        public GameDto SelectedGame { get; init; } = new();
        public bool IsLoading { get; init; } = false;
        public bool IsSaving { get; init; } = false;
    }
}
