using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingTracker.Models.Dtos;

public class PredictionDto
{
    //public int Id { get; set; }
    // public int LeagueId { get; set; }
    //public string LeagueName { get; set; }
    //public int HomeTeamId { get; set; }
    //public string HomeTeamName { get; set; }
    //public int AwayTeamId { get; set; }
    //public string AwayTeamName { get; set; }
    //public int UserId { get; set; }
    //public string UserName { get; set; }
    //public string PredictionType { get; set; }
    //public int PredictionTypeId { get; set; }
    //public DateTime KickOff { get; set; }
    //public string Tip { get; set; }
    //public string Odds { get; set; }
    //public decimal Stake { get; set; }
    //public decimal Profit { get; set; }
    //public string Status { get; set; }
    public int Id { get; set; }

    [DisplayFormat(DataFormatString = "{0:dd/MM-yyyy}")]
    public DateTime KickOff { get; set; } = DateTime.Now.Date;

    public string HomeTeam { get; set; } = string.Empty;

    public string AwayTeam { get; set; } = string.Empty;

    public string Tip { get; set; } = String.Empty;

    public string Odds { get; set; } = string.Empty;

    public string Stake { get; set; } = String.Empty;
    public decimal Profit { get; set; }
    public string Status { get; set; } = "Pending";

   // public League League { get; set; }
    public int LeagueId { get; set; }

}
