namespace CityInfo.API.Models;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; }

    public CitiesDataStore()
    {
        Cities = [
            new CityDto{
                Id = 1,
                Name = "Abuja",
                Description ="The FCT"
            },
            new CityDto{
                Id = 2,
                Name = "Lagos",
                Description ="Center of Excellence"
            },
            new CityDto{
                Id = 3,
                Name = "Ogun",
                Description ="Gateway State"
            },
            ];
    }
}
