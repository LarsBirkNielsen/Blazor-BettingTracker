using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BettingTracker.Models.Dtos;

public class PredictionDto
{

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
    public string TeamToWin { get; set; } = String.Empty ;
    public int LeagueId { get; set; } = 1;
    public string LeagueName { get; set; } = string.Empty;

}
