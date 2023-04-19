using BettingTracker.Models.Dtos;

namespace BettingTracker.Client.Services.PredictionService
{
    public interface IPredictionService
    {
        Task<IEnumerable<PredictionDto>> GetPredictions();
        Task<PredictionDto?> GetPredictionById(int id);
        Task<PredictionDto> CreatePrediction(PredictionDto predictionDto);
        Task<PredictionDto?> UpdatePrediction(int id, PredictionDto predictionDto);
        Task <PredictionDto?>DeletePrediction(int id);
        Task<List<UserDto>> GetTopUsersByProfit();
        Task<UserDto> GetUserByEmail(string email);
    }
}
