using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Interfaces;

public interface ICourseRepository
{
    public Task<List<Course>> GetAllCoursesAsync();
    public Task<Course?> GetCourseAsync(int id);
    public Task<List<Course>> GetCoursesByCategoryAsync(int categoryId);
    public Task<List<Course>> GetCoursesByStudentAsync(string studentId);
    public Task CreateCourseAsync(Course category);
    public Task<bool> SaveAllAsync();
}