using Fluxor;
using GameTracker.Client.Store.Actions;
using GameTracker.Client.Store.State;
using GameTracker.Shared.Models;

namespace GameTracker.Client.Store.Reducers
{
    public static class ToastReducer
    {
        [ReducerMethod]
        public static ToastState OnShowToast(ToastState state, SetShowToastAction action)
        {
            List<Toast> toasts = new(state.Toasts);
            Toast newToast = new()
            {
                Id = Guid.NewGuid().ToString(),
                IsError = action.IsError,
                Message = action.Message
            };
            toasts.Add(newToast);

            return state with
            {
                Toasts = toasts
            };
        }

        [ReducerMethod]
        public static ToastState OnHideToast(ToastState state, SetHideToastAction action)
        {
            List<Toast> toasts = new(state.Toasts);
            toasts = toasts.Where(t => t.Id != action.Id).ToList();

            return state with
            {
                Toasts = toasts
            };
        }
    }
}
