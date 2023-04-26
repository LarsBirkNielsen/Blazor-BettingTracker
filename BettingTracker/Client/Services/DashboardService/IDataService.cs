using BettingTracker.Models.Dtos;
using BettingTracker.Shared;

namespace BettingTracker.Client.Services.DashboardService
{
    public interface IDataService
    {
        //Task<ICollection<YearlyItem>> LoadCurrentYearExpenses();
        Task<ICollection<YearlyItem>> LoadCurrentYearProfit(HashSet<LeagueDto> leagues = null);
        Task<ICollection<YearlyItem>> LoadCurrentCumulativeYearProfit(HashSet<LeagueDto> leagues = null);
        //Task<ThreeMonthsData> LoadLast3MonthsExpenses();
        //Task<ThreeMonthsData> LoadLast3MonthsProfit();
        //Task<HitRateModel> LoadCurrentYearWinRate();
        Task<List<HitRateModel>> GetWinPercentageChartDataAsync(HashSet<LeagueDto> leagues = null);
        Task<decimal> GetTotalProfit(HashSet<LeagueDto> leagues = null);
        Task<int> GetTotalBetsPlayed(HashSet<LeagueDto> leagues = null);
        Task<decimal> GetTotalWagers(HashSet<LeagueDto> leagues = null);
        Task<decimal> GetTotalRoi(HashSet<LeagueDto> leagues = null);
        Task<(List<TeamProfitModel> BestTeams, List<TeamProfitModel> WorstTeams)> GetTopTeams(HashSet<LeagueDto> leagues = null);
    }
}
