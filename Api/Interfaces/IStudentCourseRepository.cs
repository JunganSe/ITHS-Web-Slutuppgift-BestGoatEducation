using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Interfaces;

public interface IStudentCourseRepository
{
    public Task<List<Student_Course>> GetStudentCoursesAsync(string studentId);
    public Task<Student_Course?> GetStudentCourseAsync(string studentId, int courseId);
    public Task CreateStudentCourseAsync(Student_Course studentCourse);
    public void UpdateStudentCourse(Student_Course studentCourse);
    public void DeleteStudentCourse(Student_Course studentCourse);
    public Task<bool> SaveAllAsync();
}