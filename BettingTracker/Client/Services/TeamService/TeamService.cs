using BettingTracker.Models.Dtos;
using System.Net.Http.Json;
using System.Net;

namespace BettingTracker.Client.Services.TeamService
{
    public class TeamService : ITeamService
    {
        private readonly HttpClient _httpClient;

        public TeamService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TeamDto> CreateTeam(TeamDto teamDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/team", teamDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default;
                    }

                    return await response.Content.ReadFromJsonAsync<TeamDto>();

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

        public async Task DeleteTeam(int id)
        {
            try
            {
                var teamToDelete = await GetTeamById(id);
                if (teamToDelete == null)
                {
                    throw new Exception($"League with id {id} not found.");
                }

                var response = await _httpClient.DeleteAsync($"api/team/{id}");

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

        public async Task<TeamDto?> GetTeamById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/team/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TeamDto>();
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

        public async Task<IEnumerable<TeamDto>> GetTeams()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/team");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<TeamDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<TeamDto>>();
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

        public async Task<IEnumerable<TeamDto>> GetTeamsByLeagueId(int leagueId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/team/by-league/{leagueId}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<IEnumerable<TeamDto>>();
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

        public async Task<TeamDto?> UpdateTeam(int teamId, TeamDto team)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/team/{teamId}", team);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<TeamDto>();
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
