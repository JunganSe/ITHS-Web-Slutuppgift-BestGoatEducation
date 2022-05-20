using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.Street;

namespace WestcoastEducationApi.Repositories;

public class StreetRepository : IStreetRepository
{
    private readonly Context _context;
    public StreetRepository(Context context)
    {
        _context = context;
    }



    public Task<List<Street>> GetAllStreetsAsync()
    {
        throw new NotImplementedException();
    }
    
    public Task<Street?> GetStreetAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    public Task CreateStreetAsync(PostStreetViewModel model)
    {
        throw new NotImplementedException();
    }
}