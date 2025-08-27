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

    [HttpGet("{landmarkId}")]
    public ActionResult<LandmarkDto> GetLandmark(int cityId, int landmarkId)
    {
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);

        var landmark = city.Landmarks.FirstOrDefault(x => x.Id == landmarkId);

        return city is null || landmark is null ? NotFound(landmark)
            : Ok(landmark);
    }

}
