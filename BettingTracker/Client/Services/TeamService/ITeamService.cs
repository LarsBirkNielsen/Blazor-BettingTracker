using BettingTracker.Models.Dtos;

namespace BettingTracker.Client.Services.TeamService
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetTeams();
        Task<IEnumerable<TeamDto>> GetTeamsByLeagueId(int leagueId);
        Task<TeamDto?> GetTeamById(int id);
        Task<TeamDto> CreateTeam(TeamDto teamDto);
        Task<TeamDto?> UpdateTeam(int id, TeamDto teamDto);
        Task DeleteTeam(int id);
    }
}
