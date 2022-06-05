using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.Addresses;
using WestcoastEducationAdminApp.ViewModels.AppUsers;

namespace WestcoastEducationAdminApp.Pages.Users;

[BindProperties]
public class Edit : PageModel
{
    private readonly string _apiUrl;
    private readonly IMapper _mapper;

    public EditAppUserViewModel? UserModel { get; set; }
    public List<AddressViewModel>? AddressModels { get; set; }
    public CreateAddressViewModel CreateAddressModel { get; set; } = new();

    public Edit(IConfiguration config, IMapper mapper)
    {
        _apiUrl = config.GetValue<string>("ApiUrl");
        _mapper = mapper;
    }

    public async Task OnGetAsync(string id)
    {
        var httpClient = new HttpClient();

        string userUrl = $"{_apiUrl}/AppUser/{id}";
        var userModel = await httpClient.GetFromJsonAsync<AppUserViewModel>(userUrl) ?? new AppUserViewModel();
        UserModel = _mapper.Map<EditAppUserViewModel>(userModel);

        string rolesUrl = $"{_apiUrl}/AppUser/RoleNamesByAppUser/{id}";
        var roleNames = await httpClient.GetFromJsonAsync<List<string>>(rolesUrl) ?? new List<string>() {""};
        UserModel.RoleName = roleNames[0];

        string addressUrl = $"{_apiUrl}/Address";
        AddressModels = await httpClient.GetFromJsonAsync<List<AddressViewModel>>(addressUrl) ?? new List<AddressViewModel>();
    }

    public async Task OnPostAsync()
    {
        var httpClient = new HttpClient();

        if (UserModel!.AddressId == 0)
        {
            string addressUrl = $"{_apiUrl}/Address";
            var addressResponse = await httpClient.PostAsJsonAsync(addressUrl, CreateAddressModel);
            if (!addressResponse.IsSuccessStatusCode)
            {
                ViewData["Message"] = "Failed to create address";
                return;
            }
            UserModel.AddressId = int.Parse(await addressResponse.Content.ReadAsStringAsync());
        }

        string userUrl = $"{_apiUrl}/AppUser";
        var userResponse = await httpClient.PutAsJsonAsync(userUrl, UserModel);
        if (!userResponse.IsSuccessStatusCode)
        {
            ViewData["Message"] = "Failed to update user";
            return;
        }

        var userId = await userResponse.Content.ReadAsStringAsync();
        Response.Redirect($"/Users/Details?id={userId}");
    }
}
