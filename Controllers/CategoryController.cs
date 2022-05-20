using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.ViewModels;

namespace WestcoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _repo;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }



    [HttpGet]
    public async Task<ActionResult<List<CategoryViewModel>>> GetAllCategoriesAsync()
    {
        var categories = await _repo.GetAllCategoriesAsync();
        var models = _mapper.Map<List<CategoryViewModel>>(categories);

        return Ok(models); // 200
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryViewModel>> GetCategoryAsync(int id)
    {
        var category = await _repo.GetCategoryAsync(id);
        var model = _mapper.Map<CategoryViewModel>(category);

        return (model != null)
            ? Ok(model) // 200
            : NotFound($"Fail: Find category with id {id}"); // 404
    }



    [HttpPost]
    public async Task<ActionResult> CreateCategoryAsync(PostCategoryViewModel model)
    {
        var category = _mapper.Map<Category>(model);
        await _repo.CreateCategoryAsync(category);

        return (await _repo.SaveAllAsync())
            ? StatusCode(201) // Created
            : StatusCode(500, "Fail: Create category"); // Internal server error
    }
}