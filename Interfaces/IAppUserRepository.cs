using Microsoft.AspNetCore.Identity;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Interfaces;

public interface IAppUserRepository
{
    public Task<List<AppUser>> GetAllAppUsersAsync();
    public Task<List<AppUser>> GetAllStudentsAsync();
    public Task<List<AppUser>> GetAllTeachersAsync();
    public Task<AppUser?> GetAppUserAsync(string id);
    public Task<List<AppUser>> GetStudentsByCourseAsync(int courseId);
    public Task<List<AppUser>> GetTeachersByCourseAsync(int courseId);
    public Task<List<AppUser>> GetTeachersByCompetenceAsync(int courseId);
    public Task AssignRoleAsync(AppUser appUser, string role);
    public Task ClearRolesAsync(AppUser appUser);
    public Task<List<string>> GetRoleNamesByAppUserAsync(AppUser appUser);
    public Task<bool> CreateAppUserAsync(AppUser appUser);
    public Task<bool> UpdateAppUserAsync(AppUser appUser);
    public Task<bool> DeleteAppUserAsync(AppUser appUser);
}