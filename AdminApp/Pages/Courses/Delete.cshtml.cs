using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.Courses;

namespace WestcoastEducationAdminApp.Pages.Courses;

[BindProperties]
public class Delete : PageModel
{
	private readonly string _apiUrl;

	public CourseViewModel? CourseModel { get; set; }

	public Delete(IConfiguration config)
	{
		_apiUrl = config.GetValue<string>("ApiUrl");
	}

	public async Task OnGetAsync(int id)
	{
		var httpClient = new HttpClient();
		string url = $"{_apiUrl}/Course/{id}";
		CourseModel = await httpClient.GetFromJsonAsync<CourseViewModel>(url) 
			?? new CourseViewModel();
	}

	public async Task OnPostAsync()
	{
		var httpClient = new HttpClient();
		string url = $"{_apiUrl}/Course/{CourseModel!.Id}";

		var response = await httpClient.DeleteAsync(url);
		if (response.IsSuccessStatusCode)
		{
			Response.Redirect("/Courses");
			return;
		}
		throw new Exception("Failed to delete course");
	}
}
