using BettingTracker.Models.Dtos;
using BettingTracker.Server.Data;
using BettingTracker.Server.Extensions;
using BettingTracker.Server.Services.LeagueService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BettingTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : ControllerBase
    {
        private readonly ITeamService _leagueService;

        public LeagueController(ITeamService leagueService)
        {
            _leagueService = leagueService;
        }



        [HttpGet("{id:int}")]
        public async Task<ActionResult<LeagueDto>> GetLeague(int id)
        {
            try
            {
                var league = await _leagueService.GetLeagueById(id);
                if (league == null)
                {
                    return NotFound();
                }
                var leagueDto = league.ConvertToDto();
                return Ok(leagueDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeagueDto>> GetLeagueById(int id)
        {
            try
            {
                var league = await _leagueService.GetLeagueById(id);

                if (league == null)
                {
                    return NotFound();
                }

                var leagueDto = league.ConvertToDto();
                return Ok(leagueDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeagueDto>>> GetLeagues()
        {
            try
            {
                var leagues = await _leagueService.GetLeagues();


                if (leagues == null)
                {
                    return NotFound();
                }
                else
                {
                    var leagueDto = leagues.ConvertToDto();

                    return Ok(leagueDto);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }


        [HttpPost]
        public async Task<ActionResult<LeagueDto>> CreateLeague(LeagueDto leagueDto)
        {
            try
            {
                var newLeague = await _leagueService.CreateLeague(leagueDto);
                var newLeagueDto = newLeague.ConvertToDto();

                if (newLeagueDto == null)
                {
                    return NoContent();
                }

                return CreatedAtAction(nameof(GetLeague), new { id = newLeagueDto.Id }, newLeagueDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _leagueService.DeleteLeague(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<LeagueDto>> UpdateLeague(int id, LeagueDto updatedLeague)
        {
            try
            {
                var league = await _leagueService.UpdateLeague(id, updatedLeague);
                var leagueDto = league.ConvertToDto();
                if (leagueDto == null)
                {
                    return NotFound();
                }

                return Ok(leagueDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
