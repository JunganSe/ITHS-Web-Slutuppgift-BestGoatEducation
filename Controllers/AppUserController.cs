using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels;

namespace WestcoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppUserController : ControllerBase
{
    private readonly IAppUserRepository _repo;
    private readonly IMapper _mapper;

    public AppUserController(IAppUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }



    [HttpGet]
    public async Task<ActionResult<List<AppUserViewModel>>> GetAllAppUsersAsync()
    {
        var appUsers = await _repo.GetAllAppUsersAsync();
        var models = _mapper.Map<List<AppUserViewModel>>(appUsers);

        return Ok(models); // 200
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUserViewModel>> GetAppUserAsync(string id)
    {
        var appUser = await _repo.GetAppUserAsync(id);
        var model = _mapper.Map<AppUserViewModel>(appUser);

        return (model != null)
            ? Ok(model) // 200
            : NotFound($"Fail: Find appUser with id {id}"); // 404
    }



    [HttpPost]
    public async Task<ActionResult> CreateAppUserAsync(PostAppUserViewModel model)
    {
        var appUser = _mapper.Map<AppUser>(model);
        await _repo.CreateAppUserAsync(appUser);

        return (await _repo.SaveAllAsync())
            ? StatusCode(201) // Created
            : StatusCode(500, "Fail: Create appUser"); // Internal server error
    }
}