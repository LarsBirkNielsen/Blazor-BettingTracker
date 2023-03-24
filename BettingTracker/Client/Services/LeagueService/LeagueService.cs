using BettingTracker.Models.Dtos;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Net;
using BettingTracker.Client.Services.LeagueService;

namespace BettingTracker.Client.Services.LeagueService
{
    public class LeagueService : Services.LeagueService.ILeagueService
    {
        private readonly HttpClient _httpClient;

        public LeagueService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<LeagueDto> CreateLeague(LeagueDto leagueDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/League", leagueDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default;
                    }

                    return await response.Content.ReadFromJsonAsync<LeagueDto>();

                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status:{response.StatusCode} Message -{message}");
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task DeleteLeague(int id)
        {
            try
            {
                var leagueToDelete = await GetLeagueById(id);
                if (leagueToDelete == null)
                {
                    throw new Exception($"League with id {id} not found.");
                }

                var response = await _httpClient.DeleteAsync($"api/league/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }

        public async Task<LeagueDto?> GetLeagueById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/league/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LeagueDto>();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<IEnumerable<LeagueDto>> GetLeagues()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/league");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<LeagueDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<LeagueDto>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }

            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<LeagueDto?> UpdateLeague(int leagueId, LeagueDto league)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/league/{leagueId}", league);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<LeagueDto>();
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http status code: {response.StatusCode} message: {message}");
                }
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }
    }
}
