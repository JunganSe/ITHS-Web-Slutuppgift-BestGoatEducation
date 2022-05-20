using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.ViewModels.PostalCode;

namespace WestcoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PostalCodeController : ControllerBase
{
    private readonly IPostalCodeRepository _repo;
    public PostalCodeController(IPostalCodeRepository repo)
    {
        _repo = repo;
    }
    
    
    
    [HttpGet]
    public Task<ActionResult<List<PostalCodeViewModel>>> GetAllPostalCodesAsync()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet("{id}")]
    public Task<ActionResult<PostalCodeViewModel>> GetPostalCodeAsync(int id)
    {
        throw new NotImplementedException();
    }
    
    
    
    [HttpPost]
    public Task<ActionResult> CreatePostalCodeAsync(PostPostalCodeViewModel model)
    {
        throw new NotImplementedException();
    }
}
