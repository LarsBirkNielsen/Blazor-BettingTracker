using BettingTracker.Models.Dtos;

namespace BettingTracker.Client.Services.LeagueService
{
    public interface IManageTeamsLocalStorageService
    {
        Task<IEnumerable<TeamDto>> GetCollection();
        Task RemoveCollection();
    }
}
