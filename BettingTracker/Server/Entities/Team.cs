using System.ComponentModel.DataAnnotations.Schema;

namespace BettingTracker.Server.Entities;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int LeagueId { get; set; }
    public League League { get; set; }
}
