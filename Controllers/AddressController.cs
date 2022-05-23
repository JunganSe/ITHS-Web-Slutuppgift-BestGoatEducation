using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels;

namespace WestcoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressRepository _repo;
    private readonly IMapper _mapper;
    
    public AddressController(IAddressRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }



    // GET: api/Address
    [HttpGet]
    public async Task<ActionResult<List<AddressViewModel>>> GetAllAddressesAsync()
    {
        var adresses = await _repo.GetAllAddressesAsync();
        var models = _mapper.Map<List<AddressViewModel>>(adresses);

        return Ok(models); // 200
    }

    // GET: api/Address/<id>
    [HttpGet("{id}")]
    public async Task<ActionResult<AddressViewModel>> GetAddressAsync(int id)
    {
        var address = await _repo.GetAddressAsync(id);
        var model = _mapper.Map<AddressViewModel>(address);

        return (model != null)
            ? Ok(model) // 200
            : NotFound($"Fail: Find address with id {id}"); // 404
    }



    // GET: api/Address
    [HttpPost]
    public async Task<ActionResult> CreateAddressAsync(PostAddressViewModel model)
    {
        var address = _mapper.Map<Address>(model);
        await _repo.CreateAddressAsync(address);

        return (await _repo.SaveAllAsync())
            ? StatusCode(201) // Created
            : StatusCode(500, "Fail: Create address"); // Internal server error
    }
}