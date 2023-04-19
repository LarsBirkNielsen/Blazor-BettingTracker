using BettingTracker.Client.Pages;
using BettingTracker.Models.Dtos;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;

namespace BettingTracker.Client.Services.Import
{
    public interface IImportService
    {
        Task<(HttpResponseMessage Response, List<LeagueDto> Leagues)> UploadFileAsync(Stream fileStream, string fileName);
        Task<HttpResponseMessage> SaveLeaguesAsync(List<LeagueDto> leagues);

        Task<List<PredictionDto>> UpdatePendingPredictionsAsync(IBrowserFile file);

    }
}
