﻿namespace BettingTracker.Client.Services.DashboardService
{
    public class ThreeMonthsData
    {
        public MonthlyData? CurrentMonth { get; set; }
        public MonthlyData? LastMonth { get; set; }
        public MonthlyData? PreviousMonth { get; set; }
    }
}
