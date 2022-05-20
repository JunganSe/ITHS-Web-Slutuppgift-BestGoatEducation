using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.City;
using WestcoastEducationApi.ViewModels.Country;

namespace WestcoastEducationApi.Helpers;

public class AutoMapperProfiles : AutoMapper.Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<City, CityViewModel>();
        CreateMap<PostCityViewModel, City>();
        
        CreateMap<Country, CountryViewModel>();
        CreateMap<PostCountryViewModel, Country>();
    }
}