using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels.Competences;

namespace WestcoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompetenceController : ControllerBase
{
    private readonly ICompetenceRepository _repo;
    private readonly IMapper _mapper;

    public CompetenceController(ICompetenceRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }



    // GET: api/Competence
    [HttpGet]
    public async Task<ActionResult<List<CompetenceViewModel>>> GetAllCompetencesAsync()
    {
        var competences = await _repo.GetAllCompetencesAsync();
        var models = _mapper.Map<List<CompetenceViewModel>>(competences);

        return Ok(models); // 200
    }

    // GET: api/Competence/<id>
    [HttpGet("{id}")]
    public async Task<ActionResult<CompetenceViewModel>> GetCompetenceAsync(int id)
    {
        var competence = await _repo.GetCompetenceAsync(id);
        var model = _mapper.Map<CompetenceViewModel>(competence);

        return (model != null)
            ? Ok(model) // 200
            : NotFound($"Fail: Find competence with id {id}"); // 404
    }

    // GET: api/Competence/ByTeacher/<id>
    [HttpGet("ByTeacher/{teacherId}")]
    public async Task<ActionResult<List<CompetenceViewModel>>> GetCompetencesByTeacherAsync(string teacherId)
    {
        var competences = await _repo.GetCompetencesByTeacherAsync(teacherId);
        var models = _mapper.Map<List<CompetenceViewModel>>(competences);

        return Ok(models); // 200
    }



    // POST: api/Competence
    [HttpPost]
    public async Task<ActionResult> CreateCompetenceAsync(PostCompetenceViewModel model)
    {
        var competence = _mapper.Map<Competence>(model);
        await _repo.CreateCompetenceAsync(competence);

        return (await _repo.SaveAllAsync())
            ? StatusCode(201) // Created
            : StatusCode(500, "Fail: Create competence"); // Internal server error
    }
}