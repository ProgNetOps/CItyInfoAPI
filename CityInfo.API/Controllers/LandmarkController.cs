using CityInfo.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[Route("api/cities/{cityId}/landmarks")]
[ApiController]
public class LandmarkController : ControllerBase
{

    [HttpGet]
    public ActionResult<IEnumerable<LandmarkDto>> GetLandmarks(int cityId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

        return city is null ? NotFound(city) : Ok(city.Landmarks);
    }


    [HttpGet("{landmarkId}", Name = "GetLandmark")]
    public ActionResult<LandmarkDto> GetLandmark(int cityId, int landmarkId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

        var landmark = city.Landmarks.FirstOrDefault(x => x.Id == landmarkId);

        return city is null || landmark is null
            ?NotFound(landmark)
            : Ok(landmark);
    }


    [HttpPost]
    public ActionResult<LandmarkDto> CreateLandmark(int cityId, LandmarkForCreationDto landmark)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(city =>  city.Id == cityId);

        if(city is null)
        {
            return NotFound(city);
        }

        //DEMO PURPOSE - TO BE IMPROVED
        //Find the current max landmark id, then, add 1 to it for the new landmark 
        var maxLandmarkId = CitiesDataStore.Current.Cities.SelectMany(c => c.Landmarks).Max(x=>x.Id);

        var latestLandmark = new LandmarkDto
        {
            Id = ++maxLandmarkId,
            Name = landmark.Name,
            Description = landmark.Description
        };

        city.Landmarks.Add(latestLandmark);

        return CreatedAtRoute("GetLandmark",
            new
            {
                cityId = cityId,
                landmarkId = latestLandmark.Id
            },
            latestLandmark);

    }

}