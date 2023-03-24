using BettingTracker.Models.Dtos;

namespace BettingTracker.Client.Services.LeagueService
{
    public interface ILeagueService
    {
        Task<IEnumerable<LeagueDto>> GetLeagues();
        Task<LeagueDto?> GetLeagueById(int id);
        Task<LeagueDto> CreateLeague(LeagueDto leagueDto);
        Task<LeagueDto?> UpdateLeague(int id, LeagueDto leagueDto);
        Task DeleteLeague(int id);
    }
}
