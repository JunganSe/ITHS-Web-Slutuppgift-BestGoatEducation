using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.PostalCode;

namespace WestcoastEducationApi.Interfaces;

public interface IPostalCodeRepository
{
    public Task<List<PostalCode>> GetAllPostalCodesAsync();
    public Task<PostalCode?> GetPostalCodeAsync(int id);
    public Task CreatePostalCodeAsync(PostPostalCodeViewModel model);
    public Task<bool> SaveAllAsync();
}