using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels;

namespace WestcoastEducationApi.Helpers;

public class AutoMapperProfiles : AutoMapper.Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Address, AddressViewModel>();
        CreateMap<PostAddressViewModel, Address>();
        
        CreateMap<Category, CategoryViewModel>();
        CreateMap<PostCategoryViewModel, Category>();
        
        CreateMap<Competence, CompetenceViewModel>();
        CreateMap<PostCompetenceViewModel, Competence>();
    }
}