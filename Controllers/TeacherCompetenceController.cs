using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels;

namespace WestcoastEducationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeacherCompetenceController : ControllerBase
    {
        private readonly ITeacherCompetenceRepository _repo;
        private readonly IMapper _mapper;
        
        public TeacherCompetenceController(ITeacherCompetenceRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }



        // POST: api/TeacherCompetence
        [HttpPost]
        public async Task<ActionResult> CreateTeacherCompetenceAsync(PostTeacherCompetenceViewModel model)
        {
            // TODO: Kontrollera att AppUser har rollen teacher.
            var teacherCompetence = _mapper.Map<Teacher_Competence>(model);
            await _repo.CreateTeacherCompetenceAsync(teacherCompetence);
            
            return (await _repo.SaveAllAsync())
                ? StatusCode(201) // Created
                : StatusCode(500, "Fail: Create teacherCompetence"); // Internal server error.
        }
        
        
        
        // DELETE: api/TeacherCompetence
        [HttpDelete]
        public async Task<ActionResult> DeleteTeacherCompetenceAsync(DeleteTeacherCompetenceViewModel model)
        {
            var teacherCompetence = await _repo.GetTeacherCompetenceAsync(model.TeacherId!, model.CompetenceId);
            if (teacherCompetence == null)
                return NotFound("Fail: Find teacherCompetence to delete"); // 404
            
            _repo.DeleteTeacherCompetence(teacherCompetence);
            
            return (await _repo.SaveAllAsync())
                ? NoContent() // 204
                : StatusCode(500, "Fail: Delete teacherCompetence"); // Internal server error.
        }
    }
}