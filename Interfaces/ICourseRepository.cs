using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Interfaces;

public interface ICourseRepository
{
    public Task<List<Course>> GetAllCoursesAsync();
    public Task<Course?> GetCourseAsync(int id);
    public Task<List<Course>> GetCoursesByCategoryAsync(int categoryId);
    public Task<List<Course>> GetCoursesByStudentAsync(string studentId);
    public Task<List<Course>> GetCoursesByTeacherAsync(string teacherId);
    public Task CreateCourseAsync(Course course);
    public void UpdateCourse(Course course);
    public Task DeleteCourseAsync(int id);
    public Task<bool> SaveAllAsync();
}