using System.ComponentModel.DataAnnotations.Schema;

namespace BettingTracker.Server.Entities;

public class League
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Country { get; set; } = String.Empty;
    public ICollection<Team> Teams { get; set; }

    [NotMapped]
    public IEnumerable<Team> CurrentTeams => Teams.Where(t => t.IsCurrentInLeague);

}
