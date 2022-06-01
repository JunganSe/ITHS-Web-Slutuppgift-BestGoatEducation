using WestcoastEducationAdminApp.ViewModels.Courses;

namespace WestcoastEducationAdminApp.Helpers;

public class AutoMapperProfiles : AutoMapper.Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<CourseViewModel, UpdateCourseViewModel>();
    }
}
