namespace BettingTracker.Client.Services.ExcelService
{
    public interface IExcelService
    {
        Task<byte[]> DownloadEmployeeReportAsync();
    }
}
