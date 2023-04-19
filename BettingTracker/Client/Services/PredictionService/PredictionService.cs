using BettingTracker.Client.Pages;
using BettingTracker.Models.Dtos;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace BettingTracker.Client.Services.PredictionService
{
    public class PredictionService : IPredictionService
    {
        private readonly HttpClient _httpClient;

        public PredictionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<PredictionDto> CreatePrediction(PredictionDto predictionDto)
        {
            try
            {
                //predictionDto.UserId = userId;
                var response = await _httpClient.PostAsJsonAsync("api/prediction", predictionDto);

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default;
                    }

                    return await response.Content.ReadFromJsonAsync<PredictionDto>();

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

        public async Task<PredictionDto> DeletePrediction(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/prediction/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PredictionDto>();
                }
                return default(PredictionDto);
            }
            catch (Exception)
            {
                //Log exception
                throw;
            }
        }

        public async Task<PredictionDto?> GetPredictionById(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/prediction/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<PredictionDto>();
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

        public async Task<IEnumerable<PredictionDto>> GetPredictions()
        {
            try
            {
                var response = await _httpClient.GetAsync("api/prediction");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<PredictionDto>();
                    }

                    return await response.Content.ReadFromJsonAsync<IEnumerable<PredictionDto>>();
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

        //it can take in Int count if wanted dynamic
        public async Task<List<UserDto>> GetTopUsersByProfit()
        {
            try
            {
                //var response = await _httpClient.GetAsync($"api/users/top/{count}");

                var response = await _httpClient.GetAsync("/high-score");
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<List<UserDto>>();
                    return result;
                }
                else if (response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw new ApplicationException($"Error occurred while fetching top users: {response.ReasonPhrase}");
                }
            }
            catch (Exception)
            {
                // Log exception
                throw;
            }
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var response = await _httpClient.GetAsync($"user/{email}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserDto>();
            }

            return null;
        }

        public async Task<PredictionDto?> UpdatePrediction(int predictionId, PredictionDto predictionDto)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync($"api/prediction/{predictionId}", predictionDto);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadFromJsonAsync<PredictionDto>();
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
