using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.AppUsers;
using WestcoastEducationAdminApp.ViewModels.Courses;

namespace WestcoastEducationAdminApp.Pages.Teachers;

[BindProperties]
public class Index : PageModel
{
    private readonly string _apiUrl;

    public List<AppUserViewModel> TeacherModels { get; set; } = new();

    public Index(IConfiguration config)
    {
        _apiUrl = config.GetValue<string>("ApiUrl");
    }

    public async Task OnGetAsync()
    {
        var httpClient = new HttpClient();
        string url = $"{_apiUrl}/AppUser/Teachers";
        TeacherModels = await httpClient.GetFromJsonAsync<List<AppUserViewModel>>(url) ?? new List<AppUserViewModel>();
    }
}
