using BettingTracker.Models.Dtos;

namespace BettingTracker.Client.Services.PredictionService
{
    public interface IPredictionService
    {
        Task<IEnumerable<PredictionDto>> GetPredictions();
        Task<PredictionDto?> GetPredictionById(int id);
        Task<PredictionDto> CreatePrediction(PredictionDto predictionDto);
        Task<PredictionDto?> UpdatePrediction(int id, PredictionDto predictionDto);
        Task DeletePrediction(int id);
    }
}
