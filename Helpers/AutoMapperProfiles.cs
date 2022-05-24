using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels;

namespace WestcoastEducationApi.Helpers;

public class AutoMapperProfiles : AutoMapper.Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<PostAddressViewModel, Address>();
        CreateMap<Address, AddressViewModel>();
        
        CreateMap<PostCategoryViewModel, Category>();
        CreateMap<Category, CategoryViewModel>();
        
        CreateMap<PostCompetenceViewModel, Competence>();
        CreateMap<Competence, CompetenceViewModel>();
        
        CreateMap<PostAppUserViewModel, AppUser>();
        CreateMap<AppUser, AppUserViewModel>()
            .ForMember(
                dest => dest.Address,
                options => options.MapFrom(src => string.Concat(
                    src.Address!.Street, " ",
                    src.Address.StreetNumber, ", ",
                    src.Address.PostalCode, " ",
                    src.Address.City, ", ",
                    src.Address.Country)));
        
        CreateMap<PostCourseViewModel, Course>();
        CreateMap<Course, CourseViewModel>()
            .ForMember(
                dest => dest.CategoryName,
                options => options.MapFrom(src => src.Category!.Name));
        
        CreateMap<PostStudentCourseViewModel, Student_Course>();
        CreateMap<Student_Course, StudentCourseViewModel>();
    }
}