using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationStudentApp.ViewModels;

namespace WestcoastEducationStudentApp.Pages;

[BindProperties]
public class Details : PageModel
{
    private readonly IConfiguration _config;
    private readonly string _apiUrl;

    public CourseViewModel? CourseModel { get; set; }
    public string? Message { get; set; } // TODO: Använd ViewData istället.

    public Details(IConfiguration config)
    {
        _config = config;
        _apiUrl = _config.GetValue<string>("ApiUrl");
    }

    public async Task OnGetAsync(int courseId)
    {
        var httpClient = new HttpClient();
        string url = $"{_apiUrl}/Course/{courseId}";
        CourseModel = await httpClient.GetFromJsonAsync<CourseViewModel>(url) ?? new CourseViewModel();
    }

    public async Task OnPostAsync(int courseId)
    {
        var httpClient = new HttpClient();
        string url = $"{_apiUrl}/StudentCourse";
        var postModel = new PostStudentCourseViewModel()
        {
            StudentId = HttpContext.Session.GetString("UserId"),
            CourseId = CourseModel!.Id
        };
        
        var response = await httpClient.PostAsJsonAsync(url, postModel);
        string userNameFull = HttpContext.Session.GetString("UserNameFull")
            ?? "random stranger";
        if (response.IsSuccessStatusCode)
        {
            Message = $"Registered {userNameFull} for this course.";
        }
        else
        {
            Message = $"Failed to register {userNameFull} for this course.";
        }
    }
}
