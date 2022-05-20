using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.Country;

namespace WestcoastEducationApi.Interfaces;

public interface ICountryRepository
{
    public Task<List<Country>> GetAllCountriesAsync();
    public Task<Country?> GetCountryAsync(int id);
    public Task CreateCountryAsync(Country country);
    public Task<bool> SaveAllAsync();
}