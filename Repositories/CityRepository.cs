using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Repositories;

public class CityRepository : ICityRepository
{
    private readonly Context _context;
    public CityRepository(Context context)
    {
        _context = context;
    }



    public async Task<List<City>> GetAllCitiesAsync()
    {
        return await _context.Cities.ToListAsync();
    }
    
    public async Task<City?> GetCityAsync(int id)
    {
        return await _context.Cities.FindAsync(id);
    }
    
    public async Task CreateCityAsync(City city)
    {
        await _context.Cities.AddAsync(city);
    }
    
    

    public async Task<bool> SaveAllAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}