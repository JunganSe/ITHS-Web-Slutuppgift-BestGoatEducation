using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.AppUsers;

namespace WestcoastEducationAdminApp.Pages.Users;

[BindProperties]
public class Delete : PageModel
{
	private readonly string _apiUrl;
	
	public AppUserViewModel? UserModel { get; set; }
	
	public Delete(IConfiguration config)
	{
		_apiUrl = config.GetValue<string>("ApiUrl");
	}

	public async Task OnGetAsync(string id)
	{
		var httpClient = new HttpClient();
		string url = $"{_apiUrl}/AppUser/{id}";
		UserModel = await httpClient.GetFromJsonAsync<AppUserViewModel>(url) 
			?? new AppUserViewModel();
	}
	
	public async Task OnPostAsync()
	{
		var httpClient = new HttpClient();
		string url = $"{_apiUrl}/AppUser/{UserModel!.Id}";
		
		var response = await httpClient.DeleteAsync(url);
		if (response.IsSuccessStatusCode)
		{
			Response.Redirect("/Index");
			return;
		}
        throw new Exception("Failed to delete user");
	}
}
