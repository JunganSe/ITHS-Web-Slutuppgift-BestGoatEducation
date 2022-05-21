using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels;

namespace WestcoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private readonly ICourseRepository _repo;
    private readonly IMapper _mapper;

    public CourseController(ICourseRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }



    [HttpGet]
    public async Task<ActionResult<List<CourseViewModel>>> GetAllCoursesAsync()
    {
        var courses = await _repo.GetAllCoursesAsync();
        var models = _mapper.Map<List<CourseViewModel>>(courses);

        return Ok(models); // 200
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CourseViewModel>> GetCourseAsync(int id)
    {
        var course = await _repo.GetCourseAsync(id);
        var model = _mapper.Map<CourseViewModel>(course);

        return (model != null)
            ? Ok(model) // 200
            : NotFound($"Fail: Find course with id {id}"); // 404
    }



    [HttpPost]
    public async Task<ActionResult> CreateCourseAsync(PostCourseViewModel model)
    {
        var course = _mapper.Map<Course>(model);
        await _repo.CreateCourseAsync(course);

        return (await _repo.SaveAllAsync())
            ? StatusCode(201) // Created
            : StatusCode(500, "Fail: Create course"); // Internal server error
    }
}