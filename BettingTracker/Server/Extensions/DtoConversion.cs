using BettingTracker.Models.Dtos;
using BettingTracker.Server.Entities;
using System.ComponentModel;
using System.Data;

namespace BettingTracker.Server.Extensions
{
    public static class DtoConversion
    {

        public static IEnumerable<LeagueDto> ConvertFromDbToDto(this IEnumerable<League> leagues)
        {
            var result = leagues.Select(league => new LeagueDto
            {
                Id = league.Id,
                Name = league.Name,
                Country = league.Country
            }).ToList();
            return result;
        }

        public static List<LeagueDto> ConvertToDto(this List<League> leagues)
        {
            var result = leagues.Select(league => new LeagueDto
            {
                Id = league.Id,
                Name = league.Name,
                Country = league.Country,
                Teams = league.Teams.Select(team => new TeamDto
                {
                    Id = team.Id,
                    Name = team.Name,
                    IsCurrentInLeague = team.IsCurrentInLeague
                }).ToList()
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

        public static TeamDto ConvertToDto(this Team team)
        {
            if (team == null)
            {
                return null;
            }

            return new TeamDto
            {
                Id = team.Id,
                Name = team.Name,
                LeagueId = team.LeagueId,
                LeagueName = team.League?.Name
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

        public static DataTable ConvertListToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            for (int i = 0; i < data.Count; i++)
            {
                PropertyDescriptor prop = props[i];
                table.Columns.Add(prop.Name, prop.PropertyType);
            }
            object[] values = new object[props.Count];
            foreach (T item in data)
            {
                for(int i = 0; i < values.Length; i++)
                {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }
    }
}
