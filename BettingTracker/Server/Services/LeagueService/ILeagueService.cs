using BettingTracker.Models.Dtos;
using BettingTracker.Server.Entities;

namespace BettingTracker.Server.Services.LeagueService
{
    public interface ITeamService
    {
        Task<League?> GetLeagueById(int leagueId);
        Task<List<League>> GetLeagues();
        Task<League> CreateLeague(LeagueDto league);
        Task<League?> UpdateLeague(int leagueId, LeagueDto league);
        Task<bool> DeleteLeague(int leagueId);
    }
}
