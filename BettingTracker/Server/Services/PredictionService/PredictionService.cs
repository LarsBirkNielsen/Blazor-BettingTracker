using BettingTracker.Server.Services.AuthService;
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
        private readonly IAuthService _authService;

        public PredictionService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<Prediction> CreatePrediction(PredictionDto predictionDto)
        {
            var prediction = new Prediction
            {
                KickOff = predictionDto.KickOff,
                LeagueId = predictionDto.LeagueId,
                HomeTeam = predictionDto.HomeTeam,
                AwayTeam = predictionDto.AwayTeam,
                Tip = predictionDto.Tip,
                Odds = predictionDto.Odds,
                Stake = predictionDto.Stake,
                Profit = PredictionCalculation.CalculateProfit(predictionDto.Status, predictionDto.Odds, predictionDto.Stake),
                TeamToWin = PredictionCalculation.GetTeamToWin(predictionDto.Tip, predictionDto.HomeTeam, predictionDto.AwayTeam),
                Status = predictionDto.Status,
                UserId = _authService.GetUserId(),
                UserEmail = _authService.GetUserEmail()
            };

            _context.Predictions.Add(prediction);
            await _context.SaveChangesAsync();

            return prediction;
        }

        public async Task<Prediction> DeletePrediction(int id)
        {
            var prediction = await _context.Predictions.FindAsync(id);

            if (prediction != null)
            {
                _context.Predictions.Remove(prediction);
                await _context.SaveChangesAsync();
            }

            return prediction;
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

        public async Task<List<Prediction>> GetPredictions(int userId)
        {
            //return await _context.Predictions
            //.Include(p => p.League)
            //.ToListAsync();

            var predictions = await _context.Predictions
                            .Where(x => x.UserId == userId)
                            .Include(p => p.League)
                            .ToListAsync();
            return predictions;
        }

        public async Task<List<UserDto>> GetAllUsersWithProfitAsync()
        {
            var users = await _context.Users.Include(u => u.Predictions)
                                .ThenInclude(p => p.League)
                                .ToListAsync();

            var usersWithProfit = users.Select(u =>
            {
                var validPredictions = u.Predictions.Where(p => decimal.TryParse(p.Stake, out _) && p.Status != "Pending");
                decimal totalProfit = validPredictions.Sum(p => p.Profit);
                decimal totalInvestment = validPredictions.Sum(p => decimal.Parse(p.Stake));
                decimal roi = totalInvestment != 0 ? Math.Round(((totalProfit / totalInvestment) * 100), 1) : 0;

                return new UserDto
                {
                    Id = u.Id,
                    Email = u.Email,
                    Profit = totalProfit,
                    Roi = roi
                };
            })
            .OrderByDescending(u => u.Profit)
            .ToList();

            return usersWithProfit;
        }

        public async Task<UserDto> GetUserByEmailAsync(string email)
        {
            var user = await _authService.GetUserByEmail(email);

            if (user == null)
            {
                return null;
            }

            var predictions = await _context.Predictions
                            .Where(p => p.UserId == user.Id)
                            .Include(p => p.League)
                            .ToListAsync();

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Profit = predictions.Sum(p => p.Profit)
            };
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


    }
}
