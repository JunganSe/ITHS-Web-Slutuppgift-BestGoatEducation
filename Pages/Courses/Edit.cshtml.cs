using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.Categories;
using WestcoastEducationAdminApp.ViewModels.Courses;

namespace WestcoastEducationAdminApp.Pages.Courses;

[BindProperties]
public class Edit : PageModel
{
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly string _apiUrl;

    public UpdateCourseViewModel? UpdateCourseModel { get; set; }
    public List<CategoryViewModel>? CategoryModels { get; set; }

    public Edit(IConfiguration config, IMapper mapper)
    {
        _config = config;
        _mapper = mapper;
        _apiUrl = _config.GetValue<string>("ApiUrl");
    }

    public async Task OnGetAsync(int id)
    {
        var httpClient = new HttpClient();

        string courseUrl = $"{_apiUrl}/Course/{id}";
        var courseModel = await httpClient.GetFromJsonAsync<CourseViewModel>(courseUrl) ?? new CourseViewModel();
        UpdateCourseModel = _mapper.Map<UpdateCourseViewModel>(courseModel);

        string categoryUrl = $"{_apiUrl}/Category";
        CategoryModels = await httpClient.GetFromJsonAsync<List<CategoryViewModel>>(categoryUrl) ?? new List<CategoryViewModel>();
    }

    public async Task OnPostAsync()
    {
        var httpClient = new HttpClient();
        string url = $"{_apiUrl}/Course";

        var response = await httpClient.PutAsJsonAsync(url, UpdateCourseModel);
        if (response.IsSuccessStatusCode)
        {
            Response.Redirect("/Courses");
            return;
        }
        throw new Exception("Failed to update course");
    }
}
