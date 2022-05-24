using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Repositories;

public class StudentCourseRepository : IStudentCourseRepository
{
    private readonly Context _context;

    public StudentCourseRepository(Context context)
    {
        _context = context;
    }



    public async Task<Student_Course?> GetStudentCourseAsync(string studentId, int courseId)
    {
        return await _context.Student_Courses
            .FirstOrDefaultAsync(sc => sc.StudentId == studentId && sc.CourseId == courseId);
    }



    public async Task CreateStudentCourseAsync(Student_Course studentCourse)
    {
        await _context.Student_Courses.AddAsync(studentCourse);
    }



    public void UpdateStudentCourse(Student_Course studentCourse)
    {
        _context.Student_Courses.Update(studentCourse);
    }



    public void DeleteStudentCourse(Student_Course studentCourse)
    {
        _context.Student_Courses.Remove(studentCourse);
    }



    public async Task<bool> SaveAllAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}