using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Models.Dtos;
using Blazored.LocalStorage;

namespace BettingTracker.Client.Services.PredictionService
{
    public class ManagePredictionsLocalStorageService : IManagePredictionsLocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IPredictionService _predictionService;
        private const string key = "PredictionCollection";
        private const string userIdKey = "userId";

        public ManagePredictionsLocalStorageService(ILocalStorageService localStorageService, IPredictionService predictionService)
        {
            _localStorageService = localStorageService;
            _predictionService = predictionService;
        }
        public async Task<IEnumerable<PredictionDto>> GetCollection()
        {
            return await _localStorageService.GetItemAsync<IEnumerable<PredictionDto>>(key)
                    ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await _localStorageService.RemoveItemAsync(key);
        }
        private async Task<IEnumerable<PredictionDto>> AddCollection()
        {
            var predictionCollection = await _predictionService.GetPredictions();

            if (predictionCollection != null)
            {
                await _localStorageService.SetItemAsync(key, predictionCollection);
            }
            return predictionCollection;
        }
    }
}
