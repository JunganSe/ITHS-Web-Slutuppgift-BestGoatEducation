using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels;
using WestcoastEducationApi.ViewModels.AppUsers;

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



    // GET: api/AppUser
    [HttpGet]
    public async Task<ActionResult<List<AppUserViewModel>>> GetAllAppUsersAsync()
    {
        var appUsers = await _repo.GetAllAppUsersAsync();
        var models = _mapper.Map<List<AppUserViewModel>>(appUsers);

        return Ok(models); // 200
    }
    
    // GET: api/AppUser/Students
    [HttpGet("Students")]
    public async Task<ActionResult<List<AppUserViewModel>>> GetAllStudentsAsync()
    {
        var appUsers = await _repo.GetAllStudentsAsync();
        var models = _mapper.Map<List<AppUserViewModel>>(appUsers);

        return Ok(models); // 200
    }

    // GET: api/AppUser/Teachers
    [HttpGet("Teachers")]
    public async Task<ActionResult<List<AppUserViewModel>>> GetAllTeachersAsync()
    {
        var appUsers = await _repo.GetAllTeachersAsync();
        var models = _mapper.Map<List<AppUserViewModel>>(appUsers);

        return Ok(models); // 200
    }

    // GET: api/AppUser/<id>
    [HttpGet("{id}")]
    public async Task<ActionResult<AppUserViewModel>> GetAppUserAsync(string id)
    {
        var appUser = await _repo.GetAppUserAsync(id);
        var model = _mapper.Map<AppUserViewModel>(appUser);

        return (model != null)
            ? Ok(model) // 200
            : NotFound($"Fail: Find appUser with id {id}"); // 404
    }

    // GET: api/AppUser/StudentsByCourse/<id>
    [HttpGet("StudentsByCourse/{courseId}")]
    public async Task<ActionResult<List<AppUser>>> GetStudentsByCourseAsync(int courseId)
    {
        var appUsers = await _repo.GetStudentsByCourseAsync(courseId);
        var models = _mapper.Map<List<AppUserViewModel>>(appUsers);

        return Ok(models); // 200
    }
    
    // GET: api/AppUser/TeachersByCourse/<id>
    [HttpGet("TeachersByCourse/{courseId}")]
    public async Task<ActionResult<List<AppUser>>> GetTeachersByCourseAsync(int courseId)
    {
        var appUsers = await _repo.GetTeachersByCourseAsync(courseId);
        var models = _mapper.Map<List<AppUserViewModel>>(appUsers);

        return Ok(models); // 200
    }
    
    // GET: api/AppUser/TeachersByCompetence/<id>
    [HttpGet("TeachersByCompetence/{competenceId}")]
    public async Task<ActionResult<List<AppUser>>> GetTeachersByCompetenceAsync(int competenceId)
    {
        var appUsers = await _repo.GetTeachersByCompetenceAsync(competenceId);
        var models = _mapper.Map<List<AppUserViewModel>>(appUsers);

        return Ok(models); // 200
    }
    
    // GET: api/AppUser/RoleNamesByAppUser/<userId>
    [HttpGet("RoleNamesByAppUser/{userId}")]
    public async Task<ActionResult<List<string>>> GetRoleNamesByAppUserAsync(string userId)
    {
        var appUser = await _repo.GetAppUserAsync(userId);
        if (appUser == null)
            return NotFound($"Fail: Find appUser with id {userId}");
        
        var roleNames = await  _repo.GetRoleNamesByAppUserAsync(appUser);
        return Ok(roleNames);
    }



    // POST: api/AppUser
    [HttpPost]
    public async Task<ActionResult> CreateAppUserAsync(PostAppUserViewModel model)
    {
        var appUser = _mapper.Map<AppUser>(model);
        appUser.UserName = appUser.Email;
        bool createSuccess = await _repo.CreateAppUserAsync(appUser);
        if (createSuccess && !string.IsNullOrEmpty(model.Role))
        {
            await _repo.AssignRoleAsync(appUser, model.Role);
        }

        return (createSuccess)
            ? StatusCode(201, appUser.Id) // Created
            : StatusCode(500, "Fail: Create appUser"); // Internal server error
    }
}