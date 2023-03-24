using BettingTracker.Models.Dtos;
using BettingTracker.Server.Entities;

namespace BettingTracker.Server.Services.PredictionService;

public interface IPredictionService
{
    Task<Prediction?> GetPredictionById(int predictionId);
    Task<List<Prediction>> GetPredictions();
    Task<Prediction> CreatePrediction(PredictionDto prediction);
    Task<Prediction?> UpdatePrediction(int predictionId, PredictionDto prediction);
    Task<bool> DeletePrediction(int predictionId);
}
