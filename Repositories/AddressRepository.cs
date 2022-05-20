using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;

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
        return await _context.Addresses.ToListAsync();
    }

    public async Task<Address?> GetAddressAsync(int id)
    {
        return await _context.Addresses.FindAsync(id);
    }
    
    public async Task CreateAddressAsync(Address address)
    {
        await _context.Addresses.AddAsync(address);
    }
    
    
    
    public async Task<bool> SaveAllAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}