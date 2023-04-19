using BettingTracker.Shared;

namespace BettingTracker.Client.Services.DashboardService
{
    public interface IDataService
    {
        //Task<ICollection<YearlyItem>> LoadCurrentYearExpenses();
        Task<ICollection<YearlyItem>> LoadCurrentYearProfit();
        Task<ICollection<YearlyItem>> LoadCurrentCumulativeYearProfit();
        //Task<ThreeMonthsData> LoadLast3MonthsExpenses();
        Task<ThreeMonthsData> LoadLast3MonthsProfit();
        //Task<HitRateModel> LoadCurrentYearWinRate();
        Task<List<HitRateModel>> GetWinPercentageChartDataAsync();
        Task<decimal> GetTotalProfit();
        Task<int> GetTotalBetsPlayed();
        Task<decimal> GetTotalWagers();
        Task<decimal> GetTotalRoi();
    }
}
