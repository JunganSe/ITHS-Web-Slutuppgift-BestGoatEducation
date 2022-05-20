using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.City;

namespace WestcoastEducationApi.Repositories;

public class CityRepository : ICityRepository
{
    private readonly Context _context;
    public CityRepository(Context context)
    {
        _context = context;
    }



    public Task<List<City>> GetAllCitiesAsync()
    {
        throw new NotImplementedException();
    }
    
    public Task<City?> GetCityAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    public Task CreateCityAsync(PostCityViewModel model)
    {
        throw new NotImplementedException();
    }
}