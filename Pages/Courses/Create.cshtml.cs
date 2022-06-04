
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.Categories;
using WestcoastEducationAdminApp.ViewModels.Courses;

namespace WestcoastEducationAdminApp.Pages.Courses;

[BindProperties]
public class Create : PageModel
{
    private readonly string _apiUrl;

    public CreateCourseViewModel? CourseModel { get; set; }
    public List<CategoryViewModel>? CategoryModels { get; set; }

    public Create(IConfiguration config)
    {
        _apiUrl = config.GetValue<string>("ApiUrl");
    }

    public async Task OnGetAsync()
    {
        var httpClient = new HttpClient();
        string categoryUrl = $"{_apiUrl}/Category";
        CategoryModels = await httpClient.GetFromJsonAsync<List<CategoryViewModel>>(categoryUrl) ?? new List<CategoryViewModel>();
    }
    
    public async Task OnPostAsync()
    {

        var httpClient = new HttpClient();
        string url = $"{_apiUrl}/Course";

        var response = await httpClient.PostAsJsonAsync(url, CourseModel);
        if (response.IsSuccessStatusCode)
        {
            var courseId = int.Parse(await response.Content.ReadAsStringAsync());
            Response.Redirect($"/Courses/Details?id={courseId}");
            return;
        }
        throw new Exception("Failed to Create course");
    }
}
