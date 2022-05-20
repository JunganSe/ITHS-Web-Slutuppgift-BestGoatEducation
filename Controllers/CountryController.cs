using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.ViewModels.Country;

namespace WestcoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryRepository _repo;
    public CountryController(ICountryRepository repo)
    {
        _repo = repo;
    }
    
    
    
    [HttpGet]
    public Task<ActionResult<List<CountryViewModel>>> GetAllCountriesAsync()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}")]
    public Task<ActionResult<CountryViewModel>> GetCountryAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    
    
    [HttpPost]
    public Task<ActionResult> CreateCountryAsync(PostCountryViewModel model)
    {
        throw new NotImplementedException();
    }
}
