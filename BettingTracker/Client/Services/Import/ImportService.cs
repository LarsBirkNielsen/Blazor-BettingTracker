using BettingTracker.Client.Pages;
using BettingTracker.Models.Dtos;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace BettingTracker.Client.Services.Import
{
    public class ImportService : IImportService
    {
        private readonly HttpClient _httpClient;

        public ImportService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> SaveLeaguesAsync(List<LeagueDto> leagues)
        {
            var jsonContent = JsonSerializer.Serialize(leagues);
            Console.WriteLine(jsonContent);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/import/save", content);

            return response;
        }

        public async Task<List<PredictionDto>> UpdatePendingPredictionsAsync(IBrowserFile file)
        {
            var content = new MultipartFormDataContent();
            var fileContent = new StreamContent(file.OpenReadStream());
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "file", file.Name);

            var response = await _httpClient.PostAsync("api/import/update-predictions", content);

            if (response.IsSuccessStatusCode)
            {
                var predictions = await response.Content.ReadFromJsonAsync<List<PredictionDto>>();
                return predictions;
            }
            else
            {
                throw new ApplicationException(await response.Content.ReadAsStringAsync());
            }
        }

        public async Task<(HttpResponseMessage Response, List<LeagueDto> Leagues)> UploadFileAsync(Stream fileStream, string fileName)
        {
            var content = new MultipartFormDataContent();
            content.Add(new StreamContent(fileStream), "file", fileName);

            var response = await _httpClient.PostAsync("api/import/import", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var leagues = JsonSerializer.Deserialize<List<LeagueDto>>(jsonString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return (response, leagues);
            }

            return (response, null);
        }
    }
}
