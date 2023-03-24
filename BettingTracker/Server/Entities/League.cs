using System.ComponentModel.DataAnnotations;

namespace BettingTracker.Server.Entities;

public class League
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Country { get; set; } = String.Empty;

}
