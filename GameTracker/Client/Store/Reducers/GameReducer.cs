using Fluxor;
using GameTracker.Client.Store.Actions;
using GameTracker.Client.Store.State;
using GameTracker.Shared.Models;

namespace GameTracker.Client.Store.Reducers
{
    public static class GameReducer
    {
        [ReducerMethod]
        public static GameState OnSelectGame(GameState state, SetSelectGameAction action)
        {
            return state with
            {
                SelectedGame = action.SelectedGame
            };
        }

        [ReducerMethod]
        public static GameState OnFetchGames(GameState state, SetFetchGamesAction action)
        {
            GameDto selectedGame = action.Games != null && action.Games.Count > 0 ? action.Games[0] : new();

            return state with
            {
                Games = action.Games ?? new(),
                SelectedGame = selectedGame,
                IsLoading = false
            };
        }

        [ReducerMethod]
        public static GameState OnCreateGame(GameState state, SetCreateGameAction action)
        {
            List<GameDto> games = new(state.Games);
            games.Add(action.Game);

            return state with
            {
                Games = games,
                SelectedGame = action.Game,
                IsSaving = false
            };
        }

        [ReducerMethod]
        public static GameState OnUpdateGame(GameState state, SetUpdateGameAction action)
        {
            List<GameDto> games = new(state.Games);

            games = games.Select(s => 
            {
                if (s.Id == action.Game.Id)
                {
                    s = action.Game;
                }
                return s; 
            }).ToList();

            return state with
            {
                Games = games,
                SelectedGame = action.Game,
                IsSaving = false
            };
        }

        [ReducerMethod]
        public static GameState OnDeleteGame(GameState state, SetDeleteGameAction action)
        {
            List<GameDto> games = new(state.Games);
            games = games.Where(g => g.Id != action.Id).ToList();

            return state with
            {
                Games = games,
                SelectedGame = games.Count > 0 ? games[0] : new GameDto(),
                IsSaving = false
            };
        }

        [ReducerMethod]
        public static GameState OnLoading(GameState state, SetLoadingAction action)
        {
            return state with
            {
                IsLoading = action.IsLoading
            };
        }

        [ReducerMethod]
        public static GameState OnSaving(GameState state, SetSavingAction action)
        {
            return state with
            {
                IsSaving = action.IsSaving
            };
        }
    }
}
