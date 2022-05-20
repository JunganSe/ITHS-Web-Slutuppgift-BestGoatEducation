using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Interfaces;

public interface ICategoryRepository
{
    public Task<List<Category>> GetAllCategoriesAsync();
    public Task<Category?> GetCategoryAsync(int id);
    public Task CreateCategoryAsync(Category category);
    public Task<bool> SaveAllAsync();
}