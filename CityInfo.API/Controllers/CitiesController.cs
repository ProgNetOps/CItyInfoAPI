using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers;

[ApiController]
[Route("api/cities")]
public class CitiesController(CitiesDataStore citiesDataStore) : ControllerBase
{
    private readonly CitiesDataStore _citiesDataStore = citiesDataStore;

    [HttpGet]
    public IActionResult GetCities()
    {
        return Ok(_citiesDataStore.Cities);
    }

    [HttpGet("{id}")]
    public ActionResult<CityDto> GetCity(int id)
    {
        //Find City
        var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == id);

        return city is null ? NotFound(city) : Ok(city);
    }

}
