using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.ViewModels.City;

namespace WestcoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityRepository _repo;
    public CityController(ICityRepository repo)
    {
        _repo = repo;
    }
    
    
    
    [HttpGet]
    public Task<ActionResult<List<CityViewModel>>> GetAllCitiesAsync()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}")]
    public Task<ActionResult<CityViewModel>> GetCityAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    
    
    [HttpPost]
    public Task<ActionResult> CreateCityAsync(PostCityViewModel model)
    {
        throw new NotImplementedException();
    }
}
