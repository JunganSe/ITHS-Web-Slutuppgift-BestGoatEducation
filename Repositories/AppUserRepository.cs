using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly Context _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    
    public AppUserRepository(Context context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }



    public async Task<List<AppUser>> GetAllAppUsersAsync()
    {
        return await _context.AppUsers
            .OrderBy(a => a.LastName!.ToLower())
            .Include(a => a.Address)
            .ToListAsync();
    }

    public async Task<List<AppUser>> GetAllStudentsAsync()
    {
        var students = await _userManager.GetUsersInRoleAsync("Student") as List<AppUser> ?? new List<AppUser>();
        var ids = students.Select(a => a.Id);
        return await _context.AppUsers
            .Where(a => ids.Contains(a.Id))
            .OrderBy(a => a.LastName!.ToLower())
            .Include(a => a.Address)
            .ToListAsync();
    }

    public async Task<List<AppUser>> GetAllTeachersAsync()
    {
        var teachers = await _userManager.GetUsersInRoleAsync("Teacher") as List<AppUser> ?? new List<AppUser>();
        var ids = teachers.Select(a => a.Id);
        return await _context.AppUsers
            .Where(a => ids.Contains(a.Id))
            .OrderBy(a => a.LastName!.ToLower())
            .Include(a => a.Address)
            .ToListAsync();
    }

    public async Task<AppUser?> GetAppUserAsync(string id)
    {
        return await _context.AppUsers
            .OrderBy(a => a.LastName!.ToLower())
            .Include(a => a.Address)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<AppUser>> GetStudentsByCourseAsync(int courseId)
    {
        return await _context.Student_Courses
            .Where(sc => sc.CourseId == courseId)
            .Include(sc => sc.Student)
                .ThenInclude(s => s!.Address)
            .Select(sc => sc.Student!)
            .OrderBy(a => a.LastName!.ToLower())
            .ToListAsync();
    }

    public async Task<List<AppUser>> GetTeachersByCourseAsync(int courseId)
    {
        return await _context.Teacher_Courses
            .Where(sc => sc.CourseId == courseId)
            .Include(sc => sc.Teacher)
                .ThenInclude(a => a!.Address)
            .Select(sc => sc.Teacher!)
            .OrderBy(a => a.LastName!.ToLower())
            .ToListAsync();
    }

    public async Task<List<AppUser>> GetTeachersByCompetenceAsync(int competenceId)
    {
        return await _context.Teacher_Competences
            .Where(sc => sc.CompetenceId == competenceId)
            .Include(sc => sc.Teacher)
                .ThenInclude(a => a!.Address)
            .Select(sc => sc.Teacher!)
            .OrderBy(a => a.LastName!.ToLower())
            .ToListAsync();
    }
    
    public async Task AssignRoleAsync(AppUser appUser, string role)
    {
        if (await _roleManager.RoleExistsAsync(role) && !(await _userManager.IsInRoleAsync(appUser, role)))
        {
            await _userManager.AddToRoleAsync(appUser, role);
        }
    }

    public async Task ClearRolesAsync(AppUser appUser)
    {
        var roles = await _userManager.GetRolesAsync(appUser);
        await _userManager.RemoveFromRolesAsync(appUser, roles);
    }

    public async Task<List<string>> GetRoleNamesByAppUserAsync(AppUser appUser)
    {
        return await _userManager.GetRolesAsync(appUser) as List<string> ?? new List<string>();
    }

    public async Task<bool> CreateAppUserAsync(AppUser appUser)
    {
        var result = await _userManager.CreateAsync(appUser);
        return result.Succeeded;
    }

    public async Task<bool> UpdateAppUserAsync(AppUser appUser)
    {
        var result = await _userManager.UpdateAsync(appUser);
        return result.Succeeded;
    }

    public async Task<bool> DeleteAppUserAsync(AppUser appUser)
    {
        var result = await _userManager.DeleteAsync(appUser);
        return result.Succeeded;
    }
}