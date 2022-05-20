using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels;

namespace WestcoastEducationApi.Interfaces;

public interface IAddressRepository
{
    public Task<List<Address>> GetAllAddressesAsync();
    public Task<Address?> GetAddressAsync(int id);
    public Task CreateAddressAsync(PostAddressViewModel model);
    public Task<bool> SaveAllAsync();
}