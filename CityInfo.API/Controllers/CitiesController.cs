using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities")]
public class CitiesController : ControllerBase
{
    [HttpGet]
    public IActionResult GetCities()
    {
        return Ok(CitiesDataStore.Current.Cities);
    }

    [HttpGet("{id}")]
    public ActionResult<CityDto> GetCity(int id)
    {
        //Find City
        var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == id);

        return city is null ? NotFound(city) : Ok(city);
    }

}
