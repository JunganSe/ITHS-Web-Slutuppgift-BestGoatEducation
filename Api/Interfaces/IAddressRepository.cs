using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Interfaces;

public interface IAddressRepository
{
    public Task<List<Address>> GetAllAddressesAsync();
    public Task<Address?> GetAddressAsync(int id);
    public Task CreateAddressAsync(Address address);
    public Task<bool> SaveAllAsync();
}