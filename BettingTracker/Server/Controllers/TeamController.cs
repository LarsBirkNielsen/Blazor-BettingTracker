using BettingTracker.Models.Dtos;
using BettingTracker.Server.Extensions;
using BettingTracker.Server.Services.TeamService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BettingTracker.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<TeamDto>> GetTeam(int id)
        {
            try
            {
                var team = await _teamService.GetTeamById(id);
                if (team == null)
                {
                    return NotFound();
                }
                var teamDto = team.ConvertToDto();
                return Ok(teamDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeams([FromQuery] int? leagueId = null)
        {
            try
            {
                var teams = await _teamService.GetTeamsAsync(leagueId);

                if (teams == null)
                {
                    return NotFound();
                }
                else
                {
                    var teamDtoList = teams.Select(team => team.ConvertToDto()).ToList();

                    return Ok(teamDtoList);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpGet("by-league/{leagueId}")]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeamsByLeagueId(int leagueId)
        {
            try
            {
                var teams = await _teamService.GetTeamsByLeagueId(leagueId);

                if (teams == null)
                {
                    return NotFound();
                }
                else
                {
                    var teamDtoList = teams.Select(team => team.ConvertToDto()).ToList();

                    return Ok(teamDtoList);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TeamDto>> CreateTeam(TeamDto teamDto)
        {
            try
            {
                var newTeam = await _teamService.CreateTeamAsync(teamDto);
                var newTeamDto = newTeam.ConvertToDto();

                if (newTeamDto == null)
                {
                    return NoContent();
                }

                return CreatedAtAction(nameof(GetTeam), new { id = newTeamDto.Id }, newTeamDto);
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
                var success = await _teamService.DeleteTeamAsync(id);

                if (!success)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log exception
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<TeamDto>> UpdateTeam(int id, TeamDto updatedTeam)
        {
            try
            {
                var team = await _teamService.UpdateTeamAsync(id, updatedTeam);
                var teamDto = team.ConvertToDto();
                if (teamDto == null)
                {
                    return NotFound();
                }

                return Ok(teamDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
