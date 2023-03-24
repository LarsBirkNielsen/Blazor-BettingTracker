using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Models.Dtos;
using Blazored.LocalStorage;

namespace BettingTracker.Client.Services.LeagueService
{
    public class ManageLeaguesLocalStorageService : IManageLeaguesLocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly ILeagueService _leagueService;
        private const string key = "LeagueCollection";

        public ManageLeaguesLocalStorageService(ILocalStorageService localStorageService,ILeagueService leagueService)
        {
            _localStorageService = localStorageService;
            _leagueService = leagueService;
        }
        public async Task<IEnumerable<LeagueDto>> GetCollection()
        {
            return await _localStorageService.GetItemAsync<IEnumerable<LeagueDto>>(key)
                    ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await _localStorageService.RemoveItemAsync(key);
        }
        private async Task<IEnumerable<LeagueDto>> AddCollection()
        {
            var leagueCollection = await _leagueService.GetLeagues();

            if (leagueCollection != null)
            {
                await _localStorageService.SetItemAsync(key, leagueCollection);
            }
            return leagueCollection;
        }
    }
}
