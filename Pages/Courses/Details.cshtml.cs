using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.Courses;

namespace WestcoastEducationAdminApp.Pages.Courses;

[BindProperties]
public class Details : PageModel
{
    private readonly string _apiUrl;

    public CourseViewModel? CourseModel { get; set; }

    public Details(IConfiguration config)
    {
        _apiUrl = config.GetValue<string>("ApiUrl");
    }

    public async Task OnGetAsync(int id)
    {
        var httpClient = new HttpClient();
        string url = $"{_apiUrl}/Course/{id}";
        CourseModel = await httpClient.GetFromJsonAsync<CourseViewModel>(url) ?? new CourseViewModel();
    }
}
