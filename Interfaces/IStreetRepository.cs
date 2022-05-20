using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.Street;

namespace WestcoastEducationApi.Interfaces;

public interface IStreetRepository
{
    public Task<List<Street>> GetAllStreetsAsync();
    public Task<Street?> GetStreetAsync(int id);
    public Task CreateStreetAsync(PostStreetViewModel model);
}