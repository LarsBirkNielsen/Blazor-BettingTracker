namespace BettingTracker.Client.Services.DashboardService
{
    public class YearlyItem
    {
        public string Month { get; set; } = String.Empty;
        public decimal Amount { get; set; }
        public int WinLabel { get; set; }
        public int WinValue { get; set; }

    }
}
