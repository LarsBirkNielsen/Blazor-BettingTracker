namespace BettingTracker.Client.Services.ExcelService
{
    public class ExcelService : IExcelService
    {
        private readonly HttpClient _httpClient;

        public ExcelService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<byte[]> DownloadEmployeeReportAsync()
        {
            var response = await _httpClient.GetAsync("/api/files");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
