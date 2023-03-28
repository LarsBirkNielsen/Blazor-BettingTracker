using BettingTracker.Models.Dtos;
using BettingTracker.Server.Entities;

namespace BettingTracker.Server.Services.TeamService
{
    public interface ITeamService
    {
        Task<League?> GetTeamById(int teamId);
        Task<List<League>> GetTeams();
        Task<League> CreateTeam(TeamDto team);
        Task<League?> UpdateTeam(int lteamId, TeamDto team);
        Task<bool> DeleteTeam(int teamId);
    }
}
