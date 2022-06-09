using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.StudentCourses;

namespace WestcoastEducationApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class StudentCourseController : ControllerBase
	{
		private readonly IStudentCourseRepository _repo;
		private readonly IMapper _mapper;
		
		public StudentCourseController(IStudentCourseRepository repo, IMapper mapper)
		{
			_repo = repo;
			_mapper = mapper;
		}
		
		
		
		//Get: api/StudentCourse/<studentId>
		[HttpGet("{studentId}")]
		public async Task<ActionResult<List<StudentCourseViewModel>>> GetStudentCoursesAsync(string studentId)
		{
			var studentCourses = await _repo.GetStudentCoursesAsync(studentId);
			var models = _mapper.Map<List<StudentCourseViewModel>>(studentCourses);
			
			return Ok(models); // 200
		}


		// GET: api/StudentCourse/<studentId>/<courseId>
		[HttpGet("{studentId}/{courseId}")]
		public async Task<ActionResult<StudentCourseViewModel>> GetStudentCourseAsync(string studentId, int courseId)
		{
			var studentCourse = await _repo.GetStudentCourseAsync(studentId, courseId);
			var model = _mapper.Map<StudentCourseViewModel>(studentCourse);

			return (model != null)
				? Ok(model) // 200
				: NotFound($"Fail: Find studentCourse"); // 404
		}



		// POST: api/StudentCourse
		[HttpPost]
		public async Task<ActionResult> CreateStudentCourseAsync(PostStudentCourseViewModel model)
		{
			// TODO: Kontrollera att AppUser har rollen student.
			var studentCourse = _mapper.Map<Student_Course>(model);
			await _repo.CreateStudentCourseAsync(studentCourse);
			
			return (await _repo.SaveAllAsync())
				? StatusCode(201) // Created
				: StatusCode(500, "Fail: Create studentCourse"); // Internal server error.
		}
		
		
		
		// PUT: api/StudentCourse
		[HttpPut]
		public async Task<ActionResult> UpdateStudentCourse(PutStudentCourseViewModel model)
		{
			var studentCourse = await _repo.GetStudentCourseAsync(model.StudentId!, model.CourseId);
			if (studentCourse == null)
				return NotFound("Fail: Find studentCourse to update"); // 404

			// TODO: Automappa dessa utan att StudentId och CourseId följer med.
			studentCourse.IsStarted = model.IsStarted;
			studentCourse.IsCompleted = model.IsCompleted;
			studentCourse.Grade = model.Grade;
			_repo.UpdateStudentCourse(studentCourse);
			
			return (await _repo.SaveAllAsync())
				? NoContent() // 204
				: StatusCode(500, "Fail: Update studentCourse"); // Internal server error.
		}
		
		
		
		// DELETE: api/StudentCourse
		[HttpDelete]
		public async Task<ActionResult> DeleteStudentCourseAsync(DeleteStudentCourseViewModel model)
		{
			var studentCourse = await _repo.GetStudentCourseAsync(model.StudentId!, model.CourseId);
			if (studentCourse == null)
				return NotFound("Fail: Find studentCourse to delete"); // 404
			
			_repo.DeleteStudentCourse(studentCourse);
			
			return (await _repo.SaveAllAsync())
				? NoContent() // 204
				: StatusCode(500, "Fail: Delete studentCourse"); // Internal server error.
		}
	}
}