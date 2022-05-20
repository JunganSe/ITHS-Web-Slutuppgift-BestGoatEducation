using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.City;

namespace WestcoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityRepository _repo;
    private readonly IMapper _mapper;
    
    public CityController(ICityRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }



    [HttpGet]
    public async Task<ActionResult<List<CityViewModel>>> GetAllCitiesAsync()
    {
        var cities = await _repo.GetAllCitiesAsync();
        var models = _mapper.Map<List<CityViewModel>>(cities);
        
        return Ok(models); // 200
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CityViewModel>> GetCityAsync(int id)
    {
        var city = await _repo.GetCityAsync(id);
        var model = _mapper.Map<CityViewModel>(city);

        return (model != null)
                ? Ok(model) // 200
                : NotFound($"Fail: Find city with id {id}"); // 404
    }



    [HttpPost]
    public async Task<ActionResult> CreateCityAsync(PostCityViewModel model)
    {
        var city = _mapper.Map<City>(model);
        await _repo.CreateCityAsync(city);

        return (await _repo.SaveAllAsync())
                ? StatusCode(201) // Created
                : StatusCode(500, "Fail: Create city."); // Internal server error
    }
}
