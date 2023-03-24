namespace BettingTracker.Server.Entities
{
    public class PredictionType
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public ICollection<Prediction> Predictions { get; set; }
    }
}
