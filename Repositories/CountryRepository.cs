using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.Country;

namespace WestcoastEducationApi.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly Context _context;
    public CountryRepository(Context context)
    {
        _context = context;
    }



    public Task<List<Country>> GetAllCountriesAsync()
    {
        throw new NotImplementedException();
    }
    
    public Task<Country?> GetCountryAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    public Task CreateCountryAsync(PostCountryViewModel model)
    {
        throw new NotImplementedException();
    }
}