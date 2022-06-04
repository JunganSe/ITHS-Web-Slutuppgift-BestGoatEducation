using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels;
using WestcoastEducationApi.ViewModels.Courses;

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



    // GET: api/Course
    [HttpGet]
    public async Task<ActionResult<List<CourseViewModel>>> GetAllCoursesAsync()
    {
        var courses = await _repo.GetAllCoursesAsync();
        var models = _mapper.Map<List<CourseViewModel>>(courses);

        return Ok(models); // 200
    }

    // GET: api/Course/<id>
    [HttpGet("{id}")]
    public async Task<ActionResult<CourseViewModel>> GetCourseAsync(int id)
    {
        var course = await _repo.GetCourseAsync(id);
        var model = _mapper.Map<CourseViewModel>(course);

        return (model != null)
            ? Ok(model) // 200
            : NotFound($"Fail: Find course with id {id}"); // 404
    }

    // GET: api/Course/ByCategory/<id>
    [HttpGet("ByCategory/{categoryId}")]
    public async Task<ActionResult<List<Course>>> GetCoursesByCategoryAsync(int categoryId)
    {
        var courses = await _repo.GetCoursesByCategoryAsync(categoryId);
        var models = _mapper.Map<List<CourseViewModel>>(courses);

        return Ok(models); // 200
    }

    // GET: api/Course/ByStudent/<id>
    [HttpGet("ByStudent/{studentId}")]
    public async Task<ActionResult<List<Course>>> GetCoursesByStudentAsync(string studentId)
    {
        var courses = await _repo.GetCoursesByStudentAsync(studentId);
        var models = _mapper.Map<List<CourseViewModel>>(courses);

        return Ok(models); // 200
    }

    // GET: api/Course/ByTeacher/<id>
    [HttpGet("ByTeacher/{teacherId}")]
    public async Task<ActionResult<List<Course>>> GetCoursesByTeacherAsync(string teacherId)
    {
        var courses = await _repo.GetCoursesByTeacherAsync(teacherId);
        var models = _mapper.Map<List<CourseViewModel>>(courses);

        return Ok(models); // 200
    }



    // POST: api/Course
    [HttpPost]
    public async Task<ActionResult> CreateCourseAsync(PostCourseViewModel postModel)
    {
        var course = _mapper.Map<Course>(postModel);
        await _repo.CreateCourseAsync(course);

        return (await _repo.SaveAllAsync())
            ? StatusCode(201, course.Id) // Created
            : StatusCode(500, "Fail: Create course"); // Internal server error
    }
    
    // PUT: api/Course
    [HttpPut]
    public async Task<ActionResult> UpdateCourseAsync(PutCourseViewModel putModel)
    {
        var course = await _repo.GetCourseAsync(putModel.Id);
        if (course == null)
            return NotFound("Fail: Find course to update"); // 404
        
        // TODO: Kontrollera kategori.
        
        _mapper.Map<PutCourseViewModel, Course>(putModel, course);
        
        _repo.UpdateCourse(course);

        return (await _repo.SaveAllAsync())
            ? NoContent() // 204
            : StatusCode(500, "Fail: Update course"); // Internal server error
    }
    
    // DELETE: api/Course/<id>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCourseAsync(int id)
    {
        var course = await _repo.GetCourseAsync(id);
        if (course == null)
            return NotFound("Fail: Find course to delete"); // 404

        _repo.DeleteCourse(course);

        return (await _repo.SaveAllAsync())
                ? NoContent() // 204
                : StatusCode(500, "Fail: Delete course"); // Internal server error.
    }
}