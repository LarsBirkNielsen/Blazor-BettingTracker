using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingTracker.Models.Dtos;

public class LeagueDto
{
    public int Id { get; set; } 
    public string Name { get; set; } = String.Empty;
    public string Country { get; set; } = String.Empty;
}
