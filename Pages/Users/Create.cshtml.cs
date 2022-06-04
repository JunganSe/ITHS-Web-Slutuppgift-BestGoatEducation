using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.Addresses;
using WestcoastEducationAdminApp.ViewModels.AppUsers;

namespace WestcoastEducationAdminApp.Pages.Users;

[BindProperties]
public class Create : PageModel
{
    private readonly IConfiguration _config;
    private readonly string _apiUrl;

    public CreateAppUserViewModel UserModel { get; set; } = new();
    public CreateAddressViewModel AddressModel { get; set; } = new();
    public string? Message { get; set; }
    
    public Create(IConfiguration config)
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
            Message = "Failed to create address";
            return;
        }

        UserModel.AddressId = int.Parse(await response.Content.ReadAsStringAsync());
        string userUrl = $"{_apiUrl}/AppUser";
        response = await httpClient.PostAsJsonAsync(userUrl, UserModel);
        if (!response.IsSuccessStatusCode)
        {
            Message = "Failed to create user";
            return;
        }

        var userId = await response.Content.ReadAsStringAsync();
        Response.Redirect($"/Users/Details?id={userId}");
    }
}
