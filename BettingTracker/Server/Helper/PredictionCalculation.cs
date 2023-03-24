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
            if(tip == "1")
            {
                return homeTeam;
            }
            else
            {
                return awayTeam;
            }
        }
    }
}
