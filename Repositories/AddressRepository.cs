using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.Address;

namespace WestcoastEducationApi.Repositories;

public class AddressRepository : IAddressRepository
{
    public Task<List<Address>> GetAllAddressesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Address> GetAddressAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    public Task CreateAddressAsync(PostAddressViewModel model)
    {
        throw new NotImplementedException();
    }
}