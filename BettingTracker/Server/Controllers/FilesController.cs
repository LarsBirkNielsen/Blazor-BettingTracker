using BettingTracker.Client.Services.LeagueService;
using BettingTracker.Server.Entities;
using BettingTracker.Server.Extensions;
using BettingTracker.Server.Services.ReportService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace BettingTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILeagueService _leagueService;
        private const string _contecetType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        public FilesController(IWebHostEnvironment webHostEnvironment, ILeagueService leagueService)
        {
            _webHostEnvironment = webHostEnvironment;
            _leagueService = leagueService;
        }
        [HttpGet]
        public IActionResult DownloadExcel()
        {
            byte[] bytes;
            using(var package = PredictionReport.CreateReport(_webHostEnvironment))
            {
                bytes = package.GetAsByteArray();
            }
            return File(bytes, _contecetType, $"EmployeeListReport.xlsx");
        }     
    }
}
