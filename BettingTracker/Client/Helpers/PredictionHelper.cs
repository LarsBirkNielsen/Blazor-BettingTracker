namespace BettingTracker.Client.Helper
{
    public static class PredictionHelper
    {
        public static bool HasMatchBeenPlayed(DateTime kickOff)
        {
            var nowTime = DateTime.Now;
            if (kickOff <= nowTime)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
