using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.PostalCode;

namespace WestcoastEducationApi.Repositories;

public class PostalCodeRepository : IPostalCodeRepository
{
    private readonly Context _context;
    public PostalCodeRepository(Context context)
    {
        _context = context;
    }



    public Task<List<PostalCode>> GetAllPostalCodesAsync()
    {
        throw new NotImplementedException();
    }
    
    public Task<PostalCode?> GetPostalCodeAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    public Task CreatePostalCodeAsync(PostPostalCodeViewModel model)
    {
        throw new NotImplementedException();
    }



    public async Task<bool> SaveAllAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}