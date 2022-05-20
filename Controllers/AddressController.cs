using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.ViewModels;

namespace WestcoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressRepository _repo;
    public AddressController(IAddressRepository repo)
    {
        _repo = repo;
    }
    
    
    
    [HttpGet]
    public async Task<ActionResult<List<AddressViewModel>>> GetAllAddressesAsync()
    {
        var adresses = await _repo.GetAllAddressesAsync();
        
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<AddressViewModel>> GetAddressAsync(int id)
    {
        var address = await _repo.GetAddressAsync(id);
        
        throw new NotImplementedException();
    }
    
    
    
    [HttpPost]
    public Task<ActionResult> CreateAddressAsync(PostAddressViewModel model)
    {
        throw new NotImplementedException();
    }
}