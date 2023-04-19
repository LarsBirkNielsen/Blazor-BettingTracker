using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingTracker.Models.Dtos
{
    public class MatchResultDto
    {
        public string HomeTeam { get; set; } = string.Empty;
        public string AwayTeam { get; set; } = string.Empty;
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public string HomeOdds { get; set; }
        public string HomeDrawOdds { get; set; }
        public string DrawOdds { get; set; }
        public string AwayOdds { get; set; }
        public string AwayDrawOdds { get; set; }
    }
}
