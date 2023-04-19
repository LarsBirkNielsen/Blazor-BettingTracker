using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Client.Services.TeamService;
using BettingTracker.Models.Dtos;
using Blazored.LocalStorage;

namespace BettingTracker.Client.Services.LeagueService
{
    public class ManageTeamsLocalStorageService : IManageTeamsLocalStorageService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly ITeamService _teamService;
        private const string key = "TeamCollection";

        public ManageTeamsLocalStorageService(ILocalStorageService localStorageService,ITeamService teamService)
        {
            _localStorageService = localStorageService;
            _teamService = teamService;
        }
        public async Task<IEnumerable<TeamDto>> GetCollection()
        {
            return await _localStorageService.GetItemAsync<IEnumerable<TeamDto>>(key)
                    ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await _localStorageService.RemoveItemAsync(key);
        }
        private async Task<IEnumerable<TeamDto>> AddCollection()
        {
            var teamCollection = await _teamService.GetTeams();

            if (teamCollection != null)
            {
                await _localStorageService.SetItemAsync(key, teamCollection);
            }
            return teamCollection;
        }
    }
}
