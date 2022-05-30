using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationStudentApp.ViewModels;

namespace WestcoastEducationStudentApp.Pages
{
    [BindProperties]
    public class Courses : PageModel
    {
        private readonly IConfiguration _config;
        private readonly string _apiUrl;
        
        public List<CourseViewModel> CourseModels { get; set; } = new();
        public List<CategoryViewModel> CategoryModels { get; set; } = new();
        
        public Courses(IConfiguration config)
        {
            _config = config;
            _apiUrl = _config.GetValue<string>("ApiUrl");
        }

        public async Task OnGetAsync(string? byCategory)
        {
            var httpClient = new HttpClient();
            
            string categoryUrl = $"{_apiUrl}/Category";
            CategoryModels = await httpClient.GetFromJsonAsync<List<CategoryViewModel>>(categoryUrl) ?? new List<CategoryViewModel>();
            
            string courseUrl =  (string.IsNullOrEmpty(byCategory) || byCategory == "all")
                ? $"{_apiUrl}/Course"
                : $"{_apiUrl}/Course/ByCategory/{byCategory}";
            CourseModels = await httpClient.GetFromJsonAsync<List<CourseViewModel>>(courseUrl) ?? new List<CourseViewModel>();
        }
    }
}
