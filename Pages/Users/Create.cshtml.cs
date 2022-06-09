using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.Addresses;
using WestcoastEducationAdminApp.ViewModels.AppUsers;

namespace WestcoastEducationAdminApp.Pages.Users;

[BindProperties]
public class Create : PageModel
{
	private readonly string _apiUrl;

	public CreateAppUserViewModel UserModel { get; set; } = new();
	public List<AddressViewModel>? AddressModels { get; set; }
	public CreateAddressViewModel CreateAddressModel { get; set; } = new();
	
	public Create(IConfiguration config)
	{
		_apiUrl = config.GetValue<string>("ApiUrl");
	}

	public async Task OnGetAsync()
	{
		var httpClient = new HttpClient();
		string addressUrl = $"{_apiUrl}/Address";
		AddressModels = await httpClient.GetFromJsonAsync<List<AddressViewModel>>(addressUrl)
			?? new List<AddressViewModel>();
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
		var userResponse = await httpClient.PostAsJsonAsync(userUrl, UserModel);
		if (!userResponse.IsSuccessStatusCode)
		{
			ViewData["Message"] = "Failed to create user";
			return;
		}

		var userId = await userResponse.Content.ReadAsStringAsync();
		Response.Redirect($"/Users/Details?id={userId}");
	}
}
