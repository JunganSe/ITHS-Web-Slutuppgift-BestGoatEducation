using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.AppUsers;
using WestcoastEducationAdminApp.ViewModels.Courses;

namespace WestcoastEducationAdminApp.Pages.Students;

[BindProperties]
public class Index : PageModel
{
    private readonly IConfiguration _config;
    private readonly string _apiUrl;

    public List<AppUserViewModel> StudentModels { get; set; } = new();

    public Index(IConfiguration config)
    {
        _config = config;
        _apiUrl = _config.GetValue<string>("ApiUrl");
    }

    public async Task OnGetAsync()
    {
        var httpClient = new HttpClient();
        string url = $"{_apiUrl}/AppUser/Students";
        StudentModels = await httpClient.GetFromJsonAsync<List<AppUserViewModel>>(url) ?? new List<AppUserViewModel>();
    }
}
