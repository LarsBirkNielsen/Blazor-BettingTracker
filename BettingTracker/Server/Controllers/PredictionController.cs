using BettingTracker.Models.Dtos;
using BettingTracker.Server.Entities;
using BettingTracker.Server.Extensions;
using BettingTracker.Server.Services.AuthService;
using BettingTracker.Server.Services.PredictionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BettingTracker.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PredictionController : ControllerBase
{
    private readonly IPredictionService _predictionService;
    private readonly IAuthService _authService;

    public PredictionController(IPredictionService predictionService, IAuthService authService)
    {
        _predictionService = predictionService;
        _authService = authService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PredictionDto>>> GetPredictions()
    {
        try
        {
            var userId = _authService.GetUserId();
            Console.WriteLine("UserId " + userId);
            var predicitons = await _predictionService.GetPredictions(userId);


            if (predicitons == null)
            {
                return NotFound();
            }
            else
            {
                var predictionDto = predicitons.ConvertToDto();

                return Ok(predictionDto);
            }
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                            "Error retrieving data from the database");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PredictionDto>> GetPrediction(int id)
    {
        try
        {
            var prediction = await _predictionService.GetPredictionById(id);
            if (prediction == null)
            {
                return NotFound();
            }
            var predictionDto = prediction.ConvertToDto();
            return Ok(predictionDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult<PredictionDto>> CreatePrediction(PredictionDto predictionDto)
    {
        try
        {
            var newPrediction = await _predictionService.CreatePrediction(predictionDto);
            var newPredictionDto = newPrediction.ConvertToDto();

            if (newPredictionDto == null)
            {
                return NoContent();
            }

            return CreatedAtAction(nameof(GetPrediction), new { id = newPredictionDto.Id }, newPredictionDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<PredictionDto>> UpdatePrediction(int id, PredictionDto updatedPrediction)
    {
        try
        {
            var prediction = await _predictionService.UpdatePrediction(id, updatedPrediction);
            var predictionDto = prediction.ConvertToDto();
            if (predictionDto == null)
            {
                return NotFound();
            }

            return Ok(predictionDto);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<PredictionDto>> DeletePrediction(int id)
    {
        try
        {
            var prediction = await _predictionService.DeletePrediction(id);

            if (prediction == null)
            {
                return NotFound();
            }
            return Ok(prediction);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet]
    [Route("/high-score")]
    public async Task<ActionResult<List<UserDto>>> GetTopProfitableUsers()
    {
        try
        {
            var topProfitableUsers = await _predictionService.GetTopProfitableUsersAsync();
            if (topProfitableUsers == null)
            {
                return NotFound();
            }
            return Ok(topProfitableUsers);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
