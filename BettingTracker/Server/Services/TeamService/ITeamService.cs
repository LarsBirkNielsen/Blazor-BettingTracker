using BettingTracker.Models.Dtos;
using BettingTracker.Server.Entities;

namespace BettingTracker.Server.Services.TeamService
{
    public interface ITeamService
    {
        Task<List<Team>> GetTeamsAsync(int? leagueId = null);
        Task<Team> GetTeamById(int teamId);
        Task<Team> CreateTeamAsync(TeamDto teamDto);
        Task<Team> UpdateTeamAsync(int teamId, TeamDto teamDto);
        Task<bool> DeleteTeamAsync(int teamId);
        Task<List<Team>> GetTeamsByLeagueId(int leagueId);
    }
}
