using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.AppUsers;
using WestcoastEducationAdminApp.ViewModels.Competences;
using WestcoastEducationAdminApp.ViewModels.Courses;

namespace WestcoastEducationAdminApp.Pages.Teachers;

[BindProperties]
public class Index : PageModel
{
	private readonly string _apiUrl;

	public List<AppUserViewModel>? TeacherModels { get; set; }
	public List<CompetenceViewModel>? CompetenceModels { get; set; }

	public Index(IConfiguration config)
	{
		_apiUrl = config.GetValue<string>("ApiUrl");
	}

	public async Task OnGetAsync(string? byCompetence)
	{
		var httpClient = new HttpClient();
		
		string competenceUrl = $"{_apiUrl}/Competence";
		CompetenceModels = await httpClient.GetFromJsonAsync<List<CompetenceViewModel>>(competenceUrl)
			?? new List<CompetenceViewModel>();
		
		string teacherUrl = (string.IsNullOrEmpty(byCompetence) || byCompetence == "all")
			? $"{_apiUrl}/AppUser/Teachers"
			: $"{_apiUrl}/AppUser/TeachersByCompetence/{byCompetence}";
		TeacherModels = await httpClient.GetFromJsonAsync<List<AppUserViewModel>>(teacherUrl)
			?? new List<AppUserViewModel>();
	}
}
