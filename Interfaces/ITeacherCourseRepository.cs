using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Interfaces;

public interface ITeacherCourseRepository
{
    public Task<Teacher_Course?> GetTeacherCourseAsync(string teacherId, int courseId);
    public Task CreateTeacherCourseAsync(Teacher_Course teacherCourse);
    public void DeleteTeacherCourse(Teacher_Course teacherCourse);
    public Task<bool> SaveAllAsync();
}