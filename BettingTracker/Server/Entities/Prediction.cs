using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BettingTracker.Server.Entities;

public class Prediction
{
    //public int Id { get; set; }
    //public int HomeTeamId { get; set; }
    //public Team HomeTeam { get; set; }
    //public int AwayTeamId { get; set; }
    //public Team AwayTeam { get; set; }
    //public int UserId { get; set; }
    //public User User { get; set; }
    //public PredictionType PredictionType { get; set; }
    //public int PredictionTypeId { get; set; }


    //[Required]
    //[DisplayFormat(DataFormatString = "{0:dd/MM-yyyy}")]
    //[DisplayName("Kick Off")]
    //public DateTime KickOff { get; set; } = DateTime.Now.Date;

    //[Required]
    //public string Tip { get; set; } = String.Empty;

    //[Required]
    //public string Odds { get; set; } = string.Empty;

    //[Required]
    //public decimal Stake { get; set; }
    //public decimal Profit { get; set; }
    //public string Status { get; set; } = "Pending";

    public int Id { get; set; }

    [Required]
    [DisplayFormat(DataFormatString = "{0:dd/MM-yyyy}")]
    [DisplayName("Kick Off")]
    public DateTime KickOff { get; set; } = DateTime.Now.Date;

    [Required]
    [DisplayName("Home Team")]
    public string HomeTeam { get; set; } = string.Empty;

    [Required]
    [DisplayName("Away Team")]
    public string AwayTeam { get; set; } = string.Empty;

    [Required]
    public string Tip { get; set; } = String.Empty;

    [Required]
    public string Odds { get; set; } = string.Empty;

    [Required]
    public string Stake { get; set; } = String.Empty;
    public decimal Profit { get; set; }
    public string Status { get; set; } = "Pending";

   // public string League { get; set; }
    public League League { get; set; }
    public int LeagueId { get; set; }
    public string TeamToWin { get; set; }
}
