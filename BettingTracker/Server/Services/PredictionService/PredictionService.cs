using BettingTracker.Models.Dtos;
using BettingTracker.Server.Data;
using BettingTracker.Server.Entities;
using BettingTracker.Server.Helper;
using Microsoft.EntityFrameworkCore;

namespace BettingTracker.Server.Services.PredictionService
{
    public class PredictionService : IPredictionService
    {
        private readonly DataContext _context;

        public PredictionService(DataContext context)
        {
            _context = context;
        }

        public async Task<Prediction> CreatePrediction(PredictionDto predictionDto)
        {
            var prediction = new Prediction
            {
                //HomeTeamId = predictionDto.HomeTeamId,
                //AwayTeamId = predictionDto.AwayTeamId,
                //UserId = predictionDto.UserId,
                //PredictionTypeId = predictionDto.PredictionTypeId,
                //KickOff = predictionDto.KickOff,
                //Tip = predictionDto.Tip,
                //Odds = predictionDto.Odds,
                //Stake = predictionDto.Stake,
                //Status = predictionDto.Status,

                KickOff = predictionDto.KickOff,
                LeagueId = predictionDto.LeagueId,
                HomeTeam = predictionDto.HomeTeam,
                AwayTeam = predictionDto.AwayTeam,
                Tip = predictionDto.Tip,
                Odds = predictionDto.Odds,
                Stake = predictionDto.Stake,
                Profit = PredictionCalculation.CalculateProfit(predictionDto.Status, predictionDto.Odds, predictionDto.Stake),
                TeamToWin = PredictionCalculation.GetTeamToWin(predictionDto.Tip, predictionDto.HomeTeam, predictionDto.AwayTeam),
                Status = predictionDto.Status
            };

            _context.Predictions.Add(prediction);
            await _context.SaveChangesAsync();

            return prediction;
        }

        public async Task DeletePrediction(int id)
        {
            var prediction = await _context.Predictions.FindAsync(id);

            if (prediction == null)
            {
                throw new Exception($"Prediction with ID {id} not found.");
            }

            _context.Predictions.Remove(prediction);
            await _context.SaveChangesAsync();
        }

        public async Task<Prediction> GetPredictionById(int id)
        {
            return await _context.Predictions
            //.Include(p => p.HomeTeamId)
            //.Include(p => p.AwayTeamId)
            //.Include(p => p.User)
            //.Include(p => p.PredictionType)
            .SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Prediction>> GetPredictions()
        {
            return await _context.Predictions
            //.Include(p => p.HomeTeamId)
            //.Include(p => p.AwayTeamId)
            //.Include(p => p.User)
            //.Include(p => p.PredictionType)
            .ToListAsync();
        }

        public async Task<Prediction> UpdatePrediction(int id, PredictionDto updatedPrediction)
        {

            try
            {
                var predictionToUpdate = await _context.Predictions.FindAsync(id);
                if (predictionToUpdate == null)
                {
                    throw new Exception($"League with id {id} not found.");
                }

                // Update properties of the Prediction entity based on the values in the DTO
                //predictionToUpdate.HomeTeamId = updatedPrediction.HomeTeamId;
                //predictionToUpdate.AwayTeamId = updatedPrediction.AwayTeamId;
                //predictionToUpdate.UserId = updatedPrediction.UserId;
                //predictionToUpdate.PredictionTypeId = updatedPrediction.PredictionTypeId;
                predictionToUpdate.KickOff = updatedPrediction.KickOff;
                predictionToUpdate.LeagueId = updatedPrediction.LeagueId;
                predictionToUpdate.HomeTeam = updatedPrediction.HomeTeam;
                predictionToUpdate.AwayTeam = updatedPrediction.AwayTeam;
                predictionToUpdate.Tip = updatedPrediction.Tip;
                predictionToUpdate.Odds = updatedPrediction.Odds;
                predictionToUpdate.Stake = updatedPrediction.Stake;
                predictionToUpdate.Profit = PredictionCalculation.CalculateProfit(predictionToUpdate.Status, predictionToUpdate.Odds, predictionToUpdate.Stake);
                predictionToUpdate.Status = updatedPrediction.Status;

                _context.Entry(predictionToUpdate).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return predictionToUpdate;
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }

        Task<bool> IPredictionService.DeletePrediction(int predictionId)
        {
            throw new NotImplementedException();
        }
    }
}
