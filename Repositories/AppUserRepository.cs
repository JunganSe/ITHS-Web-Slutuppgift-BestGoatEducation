using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly Context _context;
    public AppUserRepository(Context context)
    {
        _context = context;
    }



    public async Task<List<AppUser>> GetAllAppUsersAsync()
    {
        return await _context.AppUsers
            .Include(a => a.Address)
            .ToListAsync();
    }

    public async Task<AppUser?> GetAppUserAsync(string id)
    {
        return await _context.AppUsers
            .Include(a => a.Address)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<AppUser>> GetStudentsByCourseAsync(int courseId)
    {
        return await _context.Student_Courses
            .Include(sc => sc.Student)
                .ThenInclude(s => s!.Address)
            .Where(sc => sc.CourseId == courseId)
            .Select(sc => sc.Student!)
            .ToListAsync();
    }

    public async Task<List<AppUser>> GetTeachersByCourseAsync(int courseId)
    {
        return await _context.Teacher_Courses
            .Include(sc => sc.Teacher)
                .ThenInclude(a => a!.Address)
            .Where(sc => sc.CourseId == courseId)
            .Select(sc => sc.Teacher!)
            .ToListAsync();
    }

    public async Task<List<AppUser>> GetTeachersByCompetenceAsync(int competenceId)
    {
        return await _context.Teacher_Competences
            .Include(sc => sc.Teacher)
                .ThenInclude(a => a!.Address)
            .Where(sc => sc.CompetenceId == competenceId)
            .Select(sc => sc.Teacher!)
            .ToListAsync();
    }

    public async Task CreateAppUserAsync(AppUser appUser)
    {
        await _context.AppUsers.AddAsync(appUser);
    }



    public async Task<bool> SaveAllAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}