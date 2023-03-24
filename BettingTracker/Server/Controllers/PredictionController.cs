using BettingTracker.Models.Dtos;
using BettingTracker.Server.Entities;
using BettingTracker.Server.Extensions;
using BettingTracker.Server.Services.PredictionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BettingTracker.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PredictionController : ControllerBase
{
    private readonly IPredictionService _predictionService;

    public PredictionController(IPredictionService predictionService)
    {
        _predictionService = predictionService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Prediction>>> GetPredictions()
    {
        try
        {
            var predicitons = await _predictionService.GetPredictions();


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

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeletePrediction(int id)
    {
        try
        {
            await _predictionService.DeletePrediction(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            // Log exception
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
