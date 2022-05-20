using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.Address;

namespace WestcoastEducationApi.Repositories;

public class AddressRepository : IAddressRepository
{
    private readonly Context _context;
    public AddressRepository(Context context)
    {
        _context = context;
    }
    
    
    
    public async Task<List<Address>> GetAllAddressesAsync()
    {
        return await _context.Addresses
            .Include(a => a.Street)
            .Include(a => a.PostalCode)
            .Include(a => a.City)
            .Include(a => a.Country)
            .ToListAsync();
    }

    public async Task<Address?> GetAddressAsync(int id)
    {
        return await _context.Addresses
            .Include(a => a.Street)
            .Include(a => a.PostalCode)
            .Include(a => a.City)
            .Include(a => a.Country)
            .FirstOrDefaultAsync(a => a.Id == id);
    }
    
    public Task CreateAddressAsync(PostAddressViewModel model)
    {
        // TODO: Create address
        throw new NotImplementedException();
    }
    
    
    
    public async Task<bool> SaveAllAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}