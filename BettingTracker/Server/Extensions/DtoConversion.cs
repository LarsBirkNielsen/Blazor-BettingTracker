using BettingTracker.Models.Dtos;
using BettingTracker.Server.Entities;

namespace BettingTracker.Server.Extensions
{
    public static class DtoConversion
    {

        public static IEnumerable<LeagueDto> ConvertToDto(this IEnumerable<League> leagues)
        {
            var result = leagues.Select(league => new LeagueDto
            {
                Id = league.Id,
                Name = league.Name,
                Country = league.Country
            }).ToList();
            return result;
        }

        public static LeagueDto ConvertToDto(this League league)
        {
            return new LeagueDto
            {
                Id = league.Id,
                Name = league.Name,
                Country = league.Country
            };
        }

        public static PredictionDto ConvertToDto(this Prediction prediction)
        {
            return new PredictionDto
            {
                //Id = prediction.Id,
                //HomeTeamId = prediction.HomeTeamId,
                //AwayTeamId = prediction.AwayTeamId,
                //UserId = prediction.UserId,
                //PredictionTypeId = prediction.PredictionTypeId,
                //KickOff = prediction.KickOff,
                //Tip = prediction.Tip,
                //Odds = prediction.Odds,
                //Stake = prediction.Stake,
                //Profit = prediction.Profit,
                //Status = prediction.Status

                Id = prediction.Id,
                KickOff = prediction.KickOff,
                LeagueId = prediction.LeagueId,
                //LeagueName = prediction.LeagueName,
                HomeTeam = prediction.HomeTeam,
                AwayTeam = prediction.AwayTeam,
                Tip = prediction.Tip,
                Odds = prediction.Odds,
                Stake = prediction.Stake,
                Profit = prediction.Profit,
                Status = prediction.Status
            };
        }

        public static IEnumerable<PredictionDto> ConvertToDto(this IEnumerable<Prediction> predictions)
        {
            var result = predictions.Select(prediction => new PredictionDto
            {
                Id = prediction.Id,
                KickOff = prediction.KickOff,
                LeagueId = prediction.LeagueId,
                //LeagueName = prediction.LeagueName,
                HomeTeam = prediction.HomeTeam,
                AwayTeam = prediction.AwayTeam,
                Tip = prediction.Tip,
                Odds = prediction.Odds,
                Stake = prediction.Stake,
                Profit = prediction.Profit,
                Status = prediction.Status

        //HomeTeamId = prediction.HomeTeamId,
        //AwayTeamId = prediction.AwayTeamId,
        //UserId = prediction.UserId,
        //PredictionTypeId = prediction.PredictionTypeId,
        //KickOff = prediction.KickOff,
        //Tip = prediction.Tip,
        //Odds = prediction.Odds,
        //Stake = prediction.Stake,
        //Profit = prediction.Profit,
        //Status = prediction.Status
    }).ToList();
            return result;
        }
    }
}
