using BettingTracker.Models.Dtos;
using BettingTracker.Server.Entities;

namespace BettingTracker.Server.Services.ImportService;

public interface IImportService
{
    Task<List<League>> ReadLeaguesAndTeamsXlsxFileAsync(Stream fileStream);
    Task<List<LeagueDto>> SaveLeaguesAsync(List<LeagueDto> leagues);

    Task<List<MatchResultDto>> ReadMatchResultsXlsxFileAsync(Stream fileStream);
    Task<List<PredictionDto>> UpdatePendingPredictionsAsync(List<PredictionDto> pendingPredictions, List<MatchResultDto> matchResults);
}
