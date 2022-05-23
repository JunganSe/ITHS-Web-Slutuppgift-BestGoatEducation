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
            .Where(sc => sc.CourseId == courseId)
            .Select(sc => sc.Student!)
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