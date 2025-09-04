namespace CityInfo.API.Models;

public class CitiesDataStore
{
    public List<CityDto> Cities { get; set; }

    //public static CitiesDataStore Current { get; } = new CitiesDataStore();

    public CitiesDataStore()
    {
        Cities = [
            new CityDto{Id= 1, 
                Name="Abuja",
                Slogan = "The FCT",
            Landmarks = [
                new LandmarkDto { Id = 1, Name="Aso Rock", Description ="The presidential villa"}
                ]},

            new CityDto {Id= 2, Name="Lagos",Slogan = "Center of Excellence",
            Landmarks = [new LandmarkDto { Id = 1, Name="National Arts Theatre, Iganmu", Description ="A monument for celebrating arts and culture"},
                new LandmarkDto { Id = 2, Name="Freedom Park", Description ="A historical site"}
                ]},

            new CityDto{Id= 3, Name="Ogun",Slogan = "Gateway State" ,
            Landmarks = [new LandmarkDto { Id = 1, Name="Olumo Rock", Description ="A place of refuge for ancient Egba people."}
                ]},

            new CityDto{Id= 4, Name="Ondo",Slogan = "Sunshine State",
            Landmarks = [
                new LandmarkDto { Id = 1, Name="Idanre Hill", Description ="A stunning natural formation"}
                ]},

            new CityDto{Id= 5, Name="Ekiti",Slogan = "Fountain of Knowledge" , 
                Landmarks =[new LandmarkDto { Id = 1, Name = "Ikogosi Warm Spring", Description = "A spring of warm water" }]
            }];
    }
}
