using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly Context _context;
    public CategoryRepository(Context context)
    {
        _context = context;
    }



    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetCategoryAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }

    public async Task CreateCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
    }



    public async Task<bool> SaveAllAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}