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

        public async Task OnGetAsync()
        {
            string url = $"{_apiUrl}/AppUser/Students";
            var httpClient = new HttpClient();
            AppUsers = await httpClient.GetFromJsonAsync<List<AppUserViewModel>>(url) ?? new List<AppUserViewModel>();
        }

        public async Task OnPostAsync(string userId, List<AppUserViewModel> appUsers)
        {
            var httpClient = new HttpClient();
            string url = $"{_apiUrl}/AppUser/{userId}";
            var userModel = await httpClient.GetFromJsonAsync<AppUserViewModel>(url);
            if (userModel == null)
                return;

            string userNameFull = $"{userModel.FirstName} {userModel.LastName}";
            HttpContext.Session.SetString("UserNameFull", userNameFull);
            HttpContext.Session.SetString("UserId", userId);
            Response.Redirect("./Index");
        }
    }
}
