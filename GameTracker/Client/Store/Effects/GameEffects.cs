using Fluxor;
using GameTracker.Client.Store.Actions;
using GameTracker.Shared.Models;
using System.Net.Http.Json;

namespace GameTracker.Client.Store.Effects
{
    public class GameEffects
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthenticationStateProvider _authProvider;
        private readonly ILogger<GameEffects> _logger;

        public GameEffects(HttpClient httpClient, CustomAuthenticationStateProvider authProvider, ILogger<GameEffects> logger)
        {
            _httpClient = httpClient;
            _authProvider = authProvider;
            _logger = logger;
        }

        [EffectMethod]
        public async Task FetchGames(FetchGamesAction action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new SetStartLoadingAction());
            try
            {
                GamesDto? games = await _httpClient.GetFromJsonAsync<GamesDto>("/api/v1/games?userId=" + action.Id);
                if (games != null && games.GamesList != null)
                {
                    dispatcher.Dispatch(new SetFetchGamesAction(games.GamesList));
                }
            } 
            catch (Exception ex)
            {
                dispatcher.Dispatch(new SetStopLoadingAction());
                ErrorHandler(ex, dispatcher);
            }
        }

        [EffectMethod]
        public async Task CreateGame(CreateGameAction action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new SetStartSavingAction());
            try
            {
                HttpResponseMessage res = await _httpClient.PostAsJsonAsync("/api/v1/games", action.Game);
                GameDto? game = await res.Content.ReadFromJsonAsync<GameDto>();
                if (game != null)
                {
                    dispatcher.Dispatch(new SetCreateGameAction(game));
                }
                dispatcher.Dispatch(new SetShowSuccessToastAction("Game added."));
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new SetStopSavingAction());
                ErrorHandler(ex, dispatcher);
            }
        }

        [EffectMethod]
        public async Task UpdateGame(UpdateGameAction action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new SetStartSavingAction());
            try
            {
                HttpResponseMessage res = await _httpClient.PutAsJsonAsync("/api/v1/games", action.Game);
                GameDto? game = await res.Content.ReadFromJsonAsync<GameDto>();
                if (game != null)
                {
                    dispatcher.Dispatch(new SetUpdateGameAction(game));
                }
                dispatcher.Dispatch(new SetShowSuccessToastAction("Game uodated."));
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new SetStopSavingAction());
                ErrorHandler(ex, dispatcher);
            }
        }

        [EffectMethod]
        public async Task DeleteGame(DeleteGameAction action, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new SetStartSavingAction());
            try
            {
                await _httpClient.DeleteAsync("/api/v1/games/" + action.Id);
                dispatcher.Dispatch(new SetDeleteGameAction(action.Id));
                dispatcher.Dispatch(new SetShowSuccessToastAction("Game deleted."));
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new SetStopSavingAction());
                ErrorHandler(ex, dispatcher);
            }
        }

        private void ErrorHandler(Exception ex, IDispatcher dispatcher)
        {
            dispatcher.Dispatch(new SetShowErrorToastAction("An error has occurred."));
            _logger.LogError(ex.Message);
        }
    }
}
