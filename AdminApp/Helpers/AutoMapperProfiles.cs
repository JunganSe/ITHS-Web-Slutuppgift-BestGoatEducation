using WestcoastEducationAdminApp.ViewModels.Addresses;
using WestcoastEducationAdminApp.ViewModels.AppUsers;
using WestcoastEducationAdminApp.ViewModels.Courses;

namespace WestcoastEducationAdminApp.Helpers;

public class AutoMapperProfiles : AutoMapper.Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CourseViewModel, EditCourseViewModel>();
        
        CreateMap<AppUserViewModel, EditAppUserViewModel>();
    }
}
