using BettingTracker.Models.Dtos;

namespace BettingTracker.Client.Services.PredictionService
{
    public interface IManagePredictionsLocalStorageService
    {
        Task<IEnumerable<PredictionDto>> GetCollection();
        Task RemoveCollection();
    }
}
