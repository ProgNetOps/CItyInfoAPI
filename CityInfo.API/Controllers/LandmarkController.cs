using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[Route("api/cities/{cityId}/landmarks")]
[ApiController]
public class LandmarkController(ILogger<LandmarkController> logger,
    IMailService mailService,
    CitiesDataStore citiesDataStore) : ControllerBase
{
    private readonly ILogger<LandmarkController> _logger = logger;
    private readonly IMailService _mailService = mailService;
    private readonly CitiesDataStore _citiesDataStore = citiesDataStore;



    [HttpGet]
    public ActionResult<IEnumerable<LandmarkDto>> GetLandmarks(int cityId)
    {
        try
        {
            //throw new Exception("Sample exception");
            var city = _citiesDataStore.Cities.FirstOrDefault(x => x.Id == cityId);

            if (city is null)
            {
                _logger.LogInformation($"City with id {cityId} was not found when accessing landmarks");
                return NotFound(city);
            }
            else
            {
                return Ok(city.Landmarks);
            }
        }
        catch (Exception ex)
        {
            string logMessage = $"Exception while getting  landmarks for city with id, {cityId}.";

            string exceptionMessage = "A problem occured while handling your request.";
            
            _logger.LogCritical(logMessage,ex);

            //Ensure the exceptionMessage is simple and it does not expose anything to the consumer.
            return StatusCode(500, exceptionMessage);
        }
    }


    [HttpGet("{landmarkId}", Name = "GetLandmark")]
    public ActionResult<LandmarkDto> GetLandmark(int cityId, int landmarkId)
    {
        var city = _citiesDataStore.Cities.FirstOrDefault(x => x.Id == cityId);

        var landmark = city?.Landmarks?.FirstOrDefault(x => x.Id == landmarkId);

        return city is null || landmark is null
            ?NotFound(landmark)
            : Ok(landmark);
    }


    [HttpPost]
    public ActionResult<LandmarkDto> CreateLandmark(int cityId, LandmarkForCreationDto landmark)
    {
        //Find city
        var city = _citiesDataStore.Cities.FirstOrDefault(city =>  city.Id == cityId);

        if(city is null)
        {
            return NotFound(city);
        }

        //DEMO PURPOSE - TO BE IMPROVED
        //Find the current max landmark id, then, add 1 to it for the new landmark 
        var maxLandmarkId = _citiesDataStore.Cities.SelectMany(c => c.Landmarks).Max(x=>x.Id);

        var latestLandmark = new LandmarkDto
        {
            Id = ++maxLandmarkId,
            Name = landmark.Name,
            Description = landmark.Description
        };

        city.Landmarks.Add(latestLandmark);

        //The first parameter is the 'Name' value passed in the Http verb of the action method referred to
        return CreatedAtRoute(nameof(GetLandmark),
            new
            {
                cityId = cityId,
                landmarkId = latestLandmark.Id
            },
            latestLandmark);

    }

    [HttpPut("{landMarkId}")]
    public ActionResult UpdateLandmarkDto(int cityId, int landMarkId, LandmarkForUpdateDto landmark)
    {
        //Find city
        var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);

        if (city is null)
        {
            return NotFound();
        }

        //Find Landmark
        var landmarkFromStore = city.Landmarks.FirstOrDefault(x => x.Id == landMarkId);

        if (landmarkFromStore is null)
        {
            return NotFound();
        }

        //Update the fields
        landmarkFromStore.Name = landmark.Name;
        landmarkFromStore.Description = landmark.Description;

        return NoContent(); //A 204 No Content

    }


    [HttpPatch("{landmarkId}")]
    public ActionResult PartiallyUpdateLandmark(int cityId, int landmarkId, JsonPatchDocument<LandmarkForUpdateDto> patchDocument)
    {
        //Find city
        var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);

        if (city is null)
        {
            return NotFound();
        }

        var landmarkFromStore = city.Landmarks.FirstOrDefault(x => x.Id ==landmarkId);

        if(landmarkFromStore is null)
        {
            return NotFound();
        }

        var landmarkToPatch = new LandmarkForUpdateDto
        {
            Name = landmarkFromStore.Name,
            Description = landmarkFromStore.Description
        };

        //Apply the patch document
        patchDocument.ApplyTo(landmarkToPatch, ModelState);

        if(ModelState.IsValid is false)
        {
            return BadRequest(ModelState);
        }

        if(TryValidateModel(landmarkToPatch) is false)
        {
            return BadRequest(ModelState);
        }

        //Update the fields
        landmarkFromStore.Name = landmarkToPatch.Name;
        landmarkFromStore.Description = landmarkToPatch.Description;


        return NoContent();
    }


    [HttpDelete("{landmarkId}")]
    public ActionResult DeleteLandmark(int cityId, int landmarkId)
    {
        //Find city
        var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == cityId);

        if (city is null)
        {
            return NotFound();
        }

        var landmarkFromStore = city.Landmarks.FirstOrDefault(x => x.Id == landmarkId);

        if (landmarkFromStore is null)
        {
            return NotFound();
        }

        city.Landmarks.Remove(landmarkFromStore);

        _mailService.Send("Landmark deleted.", $"Landmark {landmarkFromStore.Name} with Id {landmarkId} was deleted.");

        return NoContent();
    }


}