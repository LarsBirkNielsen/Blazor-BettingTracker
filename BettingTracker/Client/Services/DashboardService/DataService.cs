using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using BettingTracker.Shared;
using static System.Net.WebRequestMethods;
using BettingTracker.Models.Dtos;

namespace BettingTracker.Client.Services.DashboardService
{
    public class DataService : IDataService
    {
        private readonly HttpClient _httpClient;
        private readonly int _currentYear = DateTime.Today.Year;

        public DataService(HttpClient http)
        {
            _httpClient = http;
        }

        public async Task<ICollection<YearlyItem>> LoadCurrentYearProfit()
        {
            List<YearlyItem> yearlyItems = new List<YearlyItem>();


            var data = await _httpClient.GetFromJsonAsync<List<PredictionDto>>("api/prediction/");
            return data
                .GroupBy(prediction => prediction.KickOff.Month)
                .OrderBy(prediction => prediction.Key)
                .Select(prediction => new YearlyItem
                {
                    Month = GetMonthAsText(prediction.Key, _currentYear),
                    Amount = prediction.Sum(item => item.Profit)
                })
                .ToList();
        }

        public async Task<ICollection<YearlyItem>> LoadCurrentCumulativeYearProfit()
        {
            List<YearlyItem> yearlyItems = new List<YearlyItem>();


            var data = await _httpClient.GetFromJsonAsync<List<PredictionDto>>("api/prediction/");
            var dataPoints = data.Where(prediction => prediction.KickOff >= new DateTime(_currentYear, 1, 1)
                && prediction.KickOff <= new DateTime(_currentYear, 12, 31))
                .GroupBy(prediction => prediction.KickOff.Month)
                .OrderBy(prediction => prediction.Key)
                .Select(prediction => new YearlyItem
                {
                    Month = GetMonthAsText(prediction.Key, _currentYear),
                    Amount = prediction.Sum(item => item.Profit)
                })
                .ToList();

            var cumulativeSum = 0.0M;
            var cumulativeDataPoints = dataPoints.Select(point =>
            {
                cumulativeSum += point.Amount;
                return new YearlyItem { Month = point.Month, Amount = cumulativeSum };
            }).ToList();

            return cumulativeDataPoints;

        }

        public async Task<ThreeMonthsData> LoadLast3MonthsProfit()
        {
            var currentMonth = DateTime.Today.Month;
            var lastMonth = DateTime.Today.AddMonths(-1);
            var previousMonth = DateTime.Today.AddMonths(-2);

            return new ThreeMonthsData
            {
                CurrentMonth = new MonthlyData
                {
                    Data = await GetMonthlyEarnings(currentMonth, _currentYear),
                    Label = GetMonthAsText(currentMonth, _currentYear)
                },
                LastMonth = new MonthlyData
                {
                    Data = await GetMonthlyEarnings(lastMonth.Month, lastMonth.Year),
                    Label = GetMonthAsText(lastMonth.Month, lastMonth.Year)
                },
                PreviousMonth = new MonthlyData
                {
                    Data = await GetMonthlyEarnings(previousMonth.Month, previousMonth.Year),
                    Label = GetMonthAsText(previousMonth.Month, previousMonth.Year)
                }
            };
        }

        private async Task<ICollection<MonthlyItem>> GetMonthlyEarnings(int month, int year)
        {
            var data = await _httpClient.GetFromJsonAsync<List<PredictionDto>>("api/prediction/");

            return data.Where(earning => earning.KickOff >= new DateTime(year, month, 1)
                && earning.KickOff <= new DateTime(year, month, LastDayOfMonth(month, year)))
                .GroupBy(prediction => prediction.LeagueName)
                .Select(prediction => new MonthlyItem
                {
                    Amount = prediction.Sum(item => item.Profit),
                })
                .ToList();
        }

        private static int LastDayOfMonth(int month, int year)
        {
            return DateTime.DaysInMonth(year, month);
        }
        private static string GetMonthAsText(int month, int year)
        {
            return month switch
            {
                1 => $"January {year}",
                2 => $"February {year}",
                3 => $"March {year}",
                4 => $"April {year}",
                5 => $"May {year}",
                6 => $"June {year}",
                7 => $"July {year}",
                8 => $"August {year}",
                9 => $"September {year}",
                10 => $"October {year}",
                11 => $"November {year}",
                12 => $"December {year}",
                _ => throw new NotImplementedException(),
            };
        }

        public async Task<object> LoadCurrentYearWinRate()
        {
            var data = await _httpClient.GetFromJsonAsync<List<PredictionDto>>("api/prediction/");

            int correctPredictions = data.Count(p => p.Status.Equals("Won"));
            double winPercentage = (double)correctPredictions / data.Count * 100;

            var winPercentageSlice = new
            {
                Label = "Win Percentage",
                Value = winPercentage
            };

            return winPercentageSlice;
        }

        public async Task<List<HitRateModel>> GetWinPercentageChartDataAsync()
        {
            var data = await _httpClient.GetFromJsonAsync<List<PredictionDto>>("api/prediction/");

            int correctPredictions = data.Count(p => p.Status.Equals("Won"));
            int incorrectPredictions = data.Count(p => p.Status.Equals("Lost"));

            double winPercentage = (double)correctPredictions / (correctPredictions + incorrectPredictions) * 100;

            var doughnutChartData = new List<HitRateModel>
            {
                new HitRateModel
                {
                    Label = "Lost",
                    Value = incorrectPredictions,
                },
                new HitRateModel
                {
                    Label = "Wins",
                    Value = correctPredictions,
                }
            };
            return doughnutChartData;
        }

        public async Task<decimal> GetTotalProfit()
        {
            var predictions = await _httpClient.GetFromJsonAsync<List<PredictionDto>>("api/prediction/");
            if (predictions != null)
            {
                decimal profitSum = predictions.Where(p => p.Status != "Pending").Sum(p => p.Profit);
                return profitSum;

            }
            else
            {
                return 0;
            }
        }

        public async Task<int> GetTotalBetsPlayed()
        {
            var predictions = await _httpClient.GetFromJsonAsync<List<PredictionDto>>("api/prediction/");
            if (predictions != null)
            {
                int betsPLayed = predictions.Where(bets => bets.Status != "Pending").Count();
                return betsPLayed;
            }
            else
            {
                return 0;
            }
        }

        public async Task<decimal> GetTotalWagers()
        {
            var predictions = await _httpClient.GetFromJsonAsync<List<PredictionDto>>("api/prediction/");
            if (predictions != null)
            {
                decimal profitSum = predictions.Where(p => p.Status != "Pending").Sum(p =>
                {
                    if (decimal.TryParse(p.Stake, out decimal stakeValue))
                    {
                        return stakeValue;
                    }
                    else
                    {
                        // If the stake value is not a valid decimal, we assume it to be zero.
                        return 0;
                    }
                });
                return profitSum;
            }
            else
            {
                return 0;
            }
        }

        public async Task<decimal> GetTotalRoi()
        {
            var TotalProfit = await GetTotalProfit();
            var TotalWager = await GetTotalWagers();
            if (TotalWager > 0)
            {
                var TotalRoi = Math.Round((TotalProfit / TotalWager) * 100, 1);
                return TotalRoi;

            }
            else
            {
                return 0;
            }
        }

        public async Task<(List<TeamProfitModel> BestTeams, List<TeamProfitModel> WorstTeams)> GetTopTeams()
        {
            var data = await _httpClient.GetFromJsonAsync<List<PredictionDto>>("api/prediction/");

            var filteredData = data.Where(prediction => !string.IsNullOrWhiteSpace(prediction.TeamToWin));

            var teamProfits = filteredData
                .GroupBy(prediction => prediction.TeamToWin)
                .Select(group => new TeamProfitModel
                {
                    TeamName = group.Key,
                    Profit = group.Sum(prediction => prediction.Profit)
                })
                .ToList();

            var bestTeams = teamProfits
                .Where(teamProfit => teamProfit.Profit > 0)
                .OrderByDescending(teamProfit => teamProfit.Profit)
                .Take(3)
                .ToList();

            var worstTeams = teamProfits
                .Where(teamProfit => teamProfit.Profit < 0)
                .OrderBy(teamProfit => teamProfit.Profit)
                .Take(3)
                .ToList();

            return (bestTeams, worstTeams);
        }
    }
}
