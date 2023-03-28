﻿using BettingTracker.Models.Dtos;
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
                Id = prediction.Id,
                KickOff = prediction.KickOff,
                LeagueId = prediction.LeagueId,
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
                LeagueId = prediction.League.Id,
                LeagueName = prediction.League.Name,
                HomeTeam = prediction.HomeTeam,
                AwayTeam = prediction.AwayTeam,
                Tip = prediction.Tip,
                Odds = prediction.Odds,
                Stake = prediction.Stake,
                Profit = prediction.Profit,
                Status = prediction.Status
                

            }).ToList();

            return result;
        }

        public static UserDto ConvertToDto(this User user)
        {
            decimal totalProfit = 0;
            foreach (var prediction in user.Predictions)
            {
                totalProfit += prediction.Profit;
            }

            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                Profit = totalProfit
            };
        }

        public static List<UserDto> ConvertToDto(this List<User> users)
        {
            var result = new List<UserDto>();
            foreach (var user in users)
            {
                decimal totalProfit = 0;
                foreach (var prediction in user.Predictions)
                {
                    totalProfit += prediction.Profit;
                }
                result.Add(new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Profit = totalProfit
                });
            }
            return result;
        }
    }
}
