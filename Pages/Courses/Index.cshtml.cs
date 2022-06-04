using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.Courses;

namespace WestcoastEducationAdminApp.Pages.Courses;

[BindProperties]
public class Index : PageModel
{
    private readonly string _apiUrl;

    public List<CourseViewModel> CourseModels { get; set; } = new();

    public Index(IConfiguration config)
    {
        _apiUrl = config.GetValue<string>("ApiUrl");
    }

    public async Task OnGetAsync()
    {
        var httpClient = new HttpClient();
        string url = $"{_apiUrl}/Course";
        CourseModels = await httpClient.GetFromJsonAsync<List<CourseViewModel>>(url) ?? new List<CourseViewModel>();
    }
}
