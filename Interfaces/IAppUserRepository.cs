using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Interfaces;

public interface IAppUserRepository
{
    public Task<List<AppUser>> GetAllAppUsersAsync();
    public Task<AppUser?> GetAppUserAsync(int id);
    public Task CreateAppUserAsync(AppUser appUser);
    public Task<bool> SaveAllAsync();
}