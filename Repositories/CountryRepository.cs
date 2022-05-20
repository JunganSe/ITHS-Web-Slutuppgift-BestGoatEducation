using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly Context _context;
    public CountryRepository(Context context)
    {
        _context = context;
    }



    public async Task<List<Country>> GetAllCountriesAsync()
    {
        return await _context.Countries.ToListAsync();
    }
    
    public async Task<Country?> GetCountryAsync(int id)
    {
        return await _context.Countries.FindAsync(id);
    }
    
    public async Task CreateCountryAsync(Country country)
    {
        await _context.Countries.AddAsync(country);
    }



    public async Task<bool> SaveAllAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}