using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly Context _context;
    
    public CourseRepository(Context context)
    {
        _context = context;
    }



    public async Task<List<Course>> GetAllCoursesAsync()
    {
        return await _context.Courses
            .Include(c => c.Category)
            .OrderBy(c => c.CategoryId)
            .ToListAsync();
    }

    public async Task<Course?> GetCourseAsync(int id)
    {
        return await _context.Courses
            .Include(c => c.Category)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<List<Course>> GetCoursesByCategoryAsync(int categoryId)
    {
        return await _context.Courses
            .Include(c => c.Category)
            .Where(c => c.CategoryId == categoryId)
            .ToListAsync();
    }

    public async Task<List<Course>> GetCoursesByStudentAsync(string studentId)
    {
        return await _context.Student_Courses
            .Include(sc => sc.Course)
                .ThenInclude(c => c!.Category)
            .Where(cs => cs.StudentId == studentId)
            .Select(cs => cs.Course!)
            .OrderBy(c => c.CategoryId)
            .ToListAsync();
    }

    public async Task<List<Course>> GetCoursesByTeacherAsync(string teacherId)
    {
        return await _context.Teacher_Courses
            .Include(sc => sc.Course)
                .ThenInclude(c => c!.Category)
            .Where(cs => cs.TeacherId == teacherId)
            .Select(cs => cs.Course!)
            .OrderBy(c => c.CategoryId)
            .ToListAsync();
    }

    public async Task CreateCourseAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
    }
    public void UpdateCourse(Course course)
    {
        _context.Courses.Update(course);
    }
    
    public void DeleteCourse(Course course)
    {
        _context.Courses.Remove(course);
    }



    public async Task<bool> SaveAllAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}