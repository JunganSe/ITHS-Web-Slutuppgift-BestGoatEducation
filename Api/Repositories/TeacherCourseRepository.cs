using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Repositories;

public class TeacherCourseRepository : ITeacherCourseRepository
{
    private readonly Context _context;

    public TeacherCourseRepository(Context context)
    {
        _context = context;
    }



    public async Task<Teacher_Course?> GetTeacherCourseAsync(string teacherId, int courseId)
    {
        return await _context.Teacher_Courses
            .FirstOrDefaultAsync(sc => sc.TeacherId == teacherId && sc.CourseId == courseId);
    }



    public async Task CreateTeacherCourseAsync(Teacher_Course teacherCourse)
    {
        await _context.Teacher_Courses.AddAsync(teacherCourse);
    }



    public void DeleteTeacherCourse(Teacher_Course teacherCourse)
    {
        _context.Teacher_Courses.Remove(teacherCourse);
    }



    public async Task<bool> SaveAllAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}