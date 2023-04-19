using BettingTracker.Server.Entities;
using BettingTracker.Server.Services.ImportService;
using BettingTracker.Server.Extensions;
using Microsoft.AspNetCore.Mvc;
using BettingTracker.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using BettingTracker.Server.Data;

namespace BettingTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportController : ControllerBase
    {
        private readonly IImportService _importService;
        private readonly DataContext _context;

        public ImportController(IImportService importService, DataContext context)
        {
            _importService = importService;
            _context = context;
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportLeagues(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not selected or empty.");
            }

            using var fileStream = file.OpenReadStream();
            var leagues = await _importService.ReadLeaguesAndTeamsXlsxFileAsync(fileStream);
            var newLeaguesDto = leagues.ConvertToDto();
            await _importService.SaveLeaguesAsync(newLeaguesDto);

            return Ok(newLeaguesDto);
        }

        //[HttpPost("save")]
        //public async Task<IActionResult> SaveLeagues([FromBody] List<League> leagues)
        //{
        //    await _importService.SaveLeaguesAsync(leagues);
        //    return Ok();
        //}

        [HttpPost("update-predictions")]
        public async Task<IActionResult> UpdatePendingPredictions(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is not selected or empty.");
            }

            using var fileStream = file.OpenReadStream();
            var matchResults = await _importService.ReadMatchResultsXlsxFileAsync(fileStream);
            var pendingPredictions = await _context.Predictions
                                                   .Where(p => p.Status == "Pending")
                                                   .Select(p => new PredictionDto
                                                   {
                                                       Id = p.Id,
                                                       HomeTeam = p.HomeTeam,
                                                       AwayTeam = p.AwayTeam,
                                                       Tip = p.Tip,
                                                       Odds = p.Odds,
                                                       Stake = p.Stake,
                                                       Status = p.Status,
                                                       Profit = p.Profit
                                                   })
                                                   .ToListAsync();
            var updatedPredictions = await _importService.UpdatePendingPredictionsAsync(pendingPredictions, matchResults);
            return Ok(updatedPredictions);
        }
    }
}
