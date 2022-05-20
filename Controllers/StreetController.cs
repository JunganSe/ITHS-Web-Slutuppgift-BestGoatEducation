using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.ViewModels.Street;

namespace WestcoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StreetController : ControllerBase
{
    private readonly IStreetRepository _repo;
    public StreetController(IStreetRepository repo)
    {
        _repo = repo;
    }
    
    
    
    [HttpGet]
    public Task<ActionResult<List<StreetViewModel>>> GetAllStreetsAsync()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}")]
    public Task<ActionResult<StreetViewModel>> GetStreetAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    
    
    [HttpPost]
    public Task<ActionResult> CreateStreetAsync(PostStreetViewModel model)
    {
        throw new NotImplementedException();
    }
}
