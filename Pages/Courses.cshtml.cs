using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationStudentApp.ViewModels;

namespace WestcoastEducationStudentApp.Pages
{
    public class Courses : PageModel
    {
        private readonly IConfiguration _config;
        private readonly string _apiUrl;
        
        [BindProperty]
        public List<CourseViewModel> CourseModels { get; set; } = new();
        
        public Courses(IConfiguration config)
        {
            _config = config;
            _apiUrl = _config.GetValue<string>("ApiUrl");
        }

        public async Task OnGetAsync()
        {
            var httpClient = new HttpClient();
            string url = $"{_apiUrl}/Course";
            CourseModels = await httpClient.GetFromJsonAsync<List<CourseViewModel>>(url) ?? new List<CourseViewModel>();
        }
    }
}
