using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.City;

namespace WestcoastEducationApi.Interfaces;

public interface ICityRepository
{
    public Task<List<City>> GetAllCitiesAsync();
    public Task<City?> GetCityAsync(int id);
    public Task CreateCityAsync(PostCityViewModel model);
}