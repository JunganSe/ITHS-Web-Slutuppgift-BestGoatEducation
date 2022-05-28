using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationStudentApp.ViewModels;

namespace WestcoastEducationStudentApp.Pages
{
    public class Register : PageModel
    {
        private readonly IConfiguration _config;
        private readonly string _apiUrl;

        [BindProperty]
        public PostAppUserViewModel UserModel { get; set; } = new();
        [BindProperty]
        public PostAddressViewModel AddressModel { get; set; } = new();
        [BindProperty]
        public string? Message { get; set; }

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
            string url = $"{_apiUrl}/Address";
            var response = await httpClient.PostAsJsonAsync(url, AddressModel);

            if (!response.IsSuccessStatusCode)
            {
                Message = await response.Content.ReadAsStringAsync();
                return;
            }

            UserModel.AddressId = int.Parse(await response.Content.ReadAsStringAsync());
            url = $"{_apiUrl}/AppUser";
            response = await httpClient.PostAsJsonAsync(url, UserModel);
            if (!response.IsSuccessStatusCode)
            {
                Message = await response.Content.ReadAsStringAsync();
                return;
            }

            string userId = await response.Content.ReadAsStringAsync();

            Message = $"Address id:\n {UserModel.AddressId}, AppUser id: {userId}";
        }
    }
}
