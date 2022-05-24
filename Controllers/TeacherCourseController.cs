using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels;

namespace WestcoastEducationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherCourseController : ControllerBase
    {
        private readonly ITeacherCourseRepository _repo;
        private readonly IMapper _mapper;
        
        public TeacherCourseController(ITeacherCourseRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }



        // POST: api/TeacherCourse
        [HttpPost]
        public async Task<ActionResult> CreateTeacherCourseAsync(PostTeacherCourseViewModel model)
        {
            var teacherCourse = _mapper.Map<Teacher_Course>(model);
            await _repo.CreateTeacherCourseAsync(teacherCourse);
            
            return (await _repo.SaveAllAsync())
                ? StatusCode(201) // Created
                : StatusCode(500, "Fail: Create teacherCourse"); // Internal server error.
        }
        
        
        
        // DELETE: api/TeacherCourse
        [HttpDelete]
        public async Task<ActionResult> DeleteTeacherCourseAsync(DeleteTeacherCourseViewModel model)
        {
            var teacherCourse = await _repo.GetTeacherCourseAsync(model.TeacherId!, model.CourseId);
            if (teacherCourse == null)
                return NotFound("Fail: Find teacherCourse to delete"); // 404
            
            _repo.DeleteTeacherCourse(teacherCourse);
            
            return (await _repo.SaveAllAsync())
                ? NoContent() // 204
                : StatusCode(500, "Fail: Delete teacherCourse"); // Internal server error.
        }
    }
}