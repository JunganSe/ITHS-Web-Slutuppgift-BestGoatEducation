using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Interfaces;

public interface ICourseRepository
{
    public Task<List<Course>> GetAllCoursesAsync();
    public Task<Course?> GetCourseAsync(int id);
    public Task CreateCourseAsync(Course category);
    public Task<bool> SaveAllAsync();
}