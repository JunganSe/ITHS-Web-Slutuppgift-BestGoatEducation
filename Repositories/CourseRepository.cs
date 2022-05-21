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
        return await _context.Courses.ToListAsync();
    }

    public async Task<Course?> GetCourseAsync(int id)
    {
        return await _context.Courses.FindAsync(id);
    }

    public async Task CreateCourseAsync(Course course)
    {
        await _context.Courses.AddAsync(course);
    }



    public async Task<bool> SaveAllAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}