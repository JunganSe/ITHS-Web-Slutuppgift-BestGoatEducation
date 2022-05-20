using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.City;

namespace WestcoastEducationApi.Helpers;

public class AutoMapperProfiles : AutoMapper.Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<PostCityViewModel, City>();
    }
}