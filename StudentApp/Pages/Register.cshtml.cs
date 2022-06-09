using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationStudentApp.ViewModels;

namespace WestcoastEducationStudentApp.Pages
{
    [BindProperties]
    public class Register : PageModel
    {
        private readonly IConfiguration _config;
        private readonly string _apiUrl;

        public PostAppUserViewModel UserModel { get; set; } = new();
        public PostAddressViewModel AddressModel { get; set; } = new();

        public Register(IConfiguration config)
        {
            _config = config;
            _apiUrl = _config.GetValue<string>("ApiUrl");
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync()
        {
            var httpClient = new HttpClient();
            string addressUrl = $"{_apiUrl}/Address";
            var response = await httpClient.PostAsJsonAsync(addressUrl, AddressModel);

            if (!response.IsSuccessStatusCode)
            {
                ViewData["Message"] = "Failed to create address";
                return;
            }

            UserModel.AddressId = int.Parse(await response.Content.ReadAsStringAsync());
            UserModel.RoleName = "Student";
            string userUrl = $"{_apiUrl}/AppUser";
            response = await httpClient.PostAsJsonAsync(userUrl, UserModel);
            if (!response.IsSuccessStatusCode)
            {
                ViewData["Message"] = "Failed to create user";
                return;
            }

            Response.Redirect("/Login");
            return;
        }
    }
}
