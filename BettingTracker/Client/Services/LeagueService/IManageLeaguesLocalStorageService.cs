using BettingTracker.Models.Dtos;

namespace BettingTracker.Client.Services.LeagueService
{
    public interface IManageLeaguesLocalStorageService
    {
        Task<IEnumerable<LeagueDto>> GetCollection();
        Task RemoveCollection();
    }
}
