using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Interfaces;

public interface IAppUserRepository
{
    public Task<List<AppUser>> GetAllAppUsersAsync();
    public Task<AppUser?> GetAppUserAsync(string id);
    public Task<List<AppUser>> GetStudentsByCourseAsync(int courseId);
    public Task<List<AppUser>> GetTeachersByCourseAsync(int courseId);
    public Task CreateAppUserAsync(AppUser appUser);
    public Task<bool> SaveAllAsync();
}