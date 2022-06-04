using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.Categories;
using WestcoastEducationAdminApp.ViewModels.Courses;

namespace WestcoastEducationAdminApp.Pages.Courses;

[BindProperties]
public class Edit : PageModel
{
    private readonly string _apiUrl;
    private readonly IMapper _mapper;

    public EditCourseViewModel? CourseModel { get; set; }
    public List<CategoryViewModel>? CategoryModels { get; set; }

    public Edit(IConfiguration config, IMapper mapper)
    {
        _apiUrl = config.GetValue<string>("ApiUrl");
        _mapper = mapper;
    }

    public async Task OnGetAsync(int id)
    {
        var httpClient = new HttpClient();

        string courseUrl = $"{_apiUrl}/Course/{id}";
        var courseModel = await httpClient.GetFromJsonAsync<CourseViewModel>(courseUrl) ?? new CourseViewModel();
        CourseModel = _mapper.Map<EditCourseViewModel>(courseModel);

        string categoryUrl = $"{_apiUrl}/Category";
        CategoryModels = await httpClient.GetFromJsonAsync<List<CategoryViewModel>>(categoryUrl) ?? new List<CategoryViewModel>();
    }

    public async Task OnPostAsync()
    {
        var httpClient = new HttpClient();
        string url = $"{_apiUrl}/Course";

        var response = await httpClient.PutAsJsonAsync(url, CourseModel);
        if (response.IsSuccessStatusCode)
        {
            Response.Redirect($"/Courses/Details?id={CourseModel!.Id}");
            return;
        }
        throw new Exception("Failed to update course");
    }
}
