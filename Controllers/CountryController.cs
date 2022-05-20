using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.Country;

namespace WestcoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryRepository _repo;
    private readonly IMapper _mapper;

    public CountryController(ICountryRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }



    [HttpGet]
    public async Task<ActionResult<List<CountryViewModel>>> GetAllCountriesAsync()
    {
        var countries = await _repo.GetAllCountriesAsync();
        var models = _mapper.Map<List<CountryViewModel>>(countries);

        return Ok(models); // 200
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CountryViewModel>> GetCountryAsync(int id)
    {
        var country = await _repo.GetCountryAsync(id);
        var model = _mapper.Map<CountryViewModel>(country);

        return (model != null)
            ? Ok(model) // 200
            : NotFound($"Fail: Find country with id {id}"); // 404
    }



    [HttpPost]
    public async Task<ActionResult> CreateCountryAsync(PostCountryViewModel model)
    {
        var country = _mapper.Map<Country>(model);
        await _repo.CreateCountryAsync(country);

        return (await _repo.SaveAllAsync())
            ? StatusCode(201) // Created
            : StatusCode(500, "Fail: Create country."); // Internal server error
    }
}
