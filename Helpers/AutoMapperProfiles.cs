using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.Addresses;
using WestcoastEducationApi.ViewModels.AppUsers;
using WestcoastEducationApi.ViewModels.Categories;
using WestcoastEducationApi.ViewModels.Competences;
using WestcoastEducationApi.ViewModels.Courses;
using WestcoastEducationApi.ViewModels.StudentCourses;
using WestcoastEducationApi.ViewModels.TeacherCompetences;
using WestcoastEducationApi.ViewModels.TeacherCourses;

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
                dest => dest.AddressName,
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
        CreateMap<PutCourseViewModel, Course>();
        
        CreateMap<PostStudentCourseViewModel, Student_Course>();
        CreateMap<Student_Course, StudentCourseViewModel>();

        CreateMap<PostTeacherCourseViewModel, Teacher_Course>();

        CreateMap<PostTeacherCompetenceViewModel, Teacher_Competence>();
    }
}