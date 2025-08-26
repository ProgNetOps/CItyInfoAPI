namespace CityInfo.API.Models;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; }

    public static CitiesDataStore Current { get; } = new CitiesDataStore();

    public CitiesDataStore()
    {
        Cities = [
            new CityDto{Id= 1, Name="Abuja",Slogan = "The FCT" },
            new CityDto {Id= 2, Name="Lagos",Slogan = "Center of Excellence" },
            new CityDto{Id= 3, Name="Ogun",Slogan = "Gateway State" },
            new CityDto{Id= 4, Name="Ondo",Slogan = "Sunshine State" },
            new CityDto{Id= 5, Name="Benue",Slogan = "Food Basket of the Nation" }];
    }
}
