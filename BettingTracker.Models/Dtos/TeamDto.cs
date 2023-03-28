using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingTracker.Models.Dtos;

public class TeamDto
{
    public int Id { get; set; } 
    public string Name { get; set; } = String.Empty;
    public int LeagueId { get; set; } = 1;
    public string LeagueName { get; set; } = string.Empty;
}
