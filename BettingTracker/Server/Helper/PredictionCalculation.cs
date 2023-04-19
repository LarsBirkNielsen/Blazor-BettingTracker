namespace BettingTracker.Server.Helper
{
    public class PredictionCalculation
    {
        public static decimal CalculateProfit(string status, string odds, string stake)
        {
            double profit = 0;
            switch (status)
            {
                case "Won":
                    profit = (Convert.ToDouble(odds) * Convert.ToDouble(stake)) - Convert.ToDouble(stake);
                    break;

                case "Lost":
                    profit = Convert.ToDouble(stake) * -1;
                    break;
                case "Pending":
                    profit = 0;
                    break;
                default:
                    break;
            }
            return (decimal)profit;
        }

        public static string GetTeamToWin(string tip, string homeTeam, string awayTeam)
        {
            switch (tip)
            {
                case "1":
                case "1x":
                    return homeTeam;
                case "2":
                case "x2":
                    return awayTeam;
                case "x":
                case "X":
                    return "No Team";
                default:
                    return default;
            }
        }
    }
}
