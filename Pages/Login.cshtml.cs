using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationStudentApp.ViewModels;

namespace WestcoastEducationStudentApp.Pages
{
    public class Login : PageModel
    {
        private readonly IConfiguration _config;
        private readonly string _apiUrl;
        
        [BindProperty]
        public List<AppUserViewModel>? AppUsers { get; set; }
        
        public Login(IConfiguration config)
        {
            _config = config;
            _apiUrl = _config.GetValue<string>("ApiUrl");
        }

        public async Task OnGet()
        {
            string url = $"{_apiUrl}/AppUser";
            var httpClient = new HttpClient();
            AppUsers = await httpClient.GetFromJsonAsync<List<AppUserViewModel>>(url) ?? new List<AppUserViewModel>();
        }

        public void OnPost(string userId, List<AppUserViewModel> appUsers)
        {
            // HttpContext.Session.SetString("UserId", userId);
        }
    }
}
