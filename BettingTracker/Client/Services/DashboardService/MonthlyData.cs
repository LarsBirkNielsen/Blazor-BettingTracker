namespace BettingTracker.Client.Services.DashboardService
{
    public class MonthlyData
    {
        public ICollection<MonthlyItem> Data { get; set; }
        public string Label { get; set; } = String.Empty;
    }
}
