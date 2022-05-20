using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.ViewModels.Address;

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
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<AddressViewModel>> GetAddressAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    
    
    [HttpPost]
    public async Task<ActionResult> CreateAddressAsync()
    {
        throw new NotImplementedException();
    }
}