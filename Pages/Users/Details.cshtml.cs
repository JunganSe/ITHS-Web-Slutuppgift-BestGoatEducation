using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WestcoastEducationAdminApp.ViewModels.AppUsers;
using WestcoastEducationAdminApp.ViewModels.Competences;
using WestcoastEducationAdminApp.ViewModels.Courses;
using WestcoastEducationAdminApp.ViewModels.StudentCourses;

namespace WestcoastEducationAdminApp.Pages.Users;

[BindProperties]
public class Details : PageModel
{
	private readonly string _apiUrl;

	public AppUserViewModel? UserModel { get; set; }
	public List<CourseViewModel>? CourseModels { get; set; }
	public Dictionary<int, string> CourseGrades { get; set; } = new();
	public List<CompetenceViewModel>? CompetenceModels { get; set; }

	public Details(IConfiguration config)
	{
		_apiUrl = config.GetValue<string>("ApiUrl");
	}

	public async Task OnGetAsync(string id)
	{
		var httpClient = new HttpClient();

		string userUrl = $"{_apiUrl}/AppUser/{id}";
		UserModel = await httpClient.GetFromJsonAsync<AppUserViewModel>(userUrl) 
			?? new AppUserViewModel();

		string rolesUrl = $"{_apiUrl}/AppUser/RoleNamesByAppUser/{id}";
		var roleNames = await httpClient.GetFromJsonAsync<List<string>>(rolesUrl) 
			?? new List<string>();
		UserModel.RoleName = (roleNames.Any())
			? roleNames[0]
			: "n/a";

		if (UserModel.RoleName == "Student")
		{
			string coursesUrl = $"{_apiUrl}/Course/ByStudent/{id}";
			CourseModels = await httpClient.GetFromJsonAsync<List<CourseViewModel>>(coursesUrl)
				?? new List<CourseViewModel>();
			
			string studentCoursesUrl = $"{_apiUrl}/StudentCourse/{id}";
			var studentCourses = await httpClient.GetFromJsonAsync<List<StudentCourseViewModel>>(studentCoursesUrl)
				?? new List<StudentCourseViewModel>();
			foreach (var sc in studentCourses)
			{
				CourseGrades.Add(sc.CourseId, sc.Grade ?? "n/a");
			}
		}
		else if (UserModel.RoleName == "Teacher")
		{
			string coursesUrl = $"{_apiUrl}/Course/ByTeacher/{id}";
			CourseModels = await httpClient.GetFromJsonAsync<List<CourseViewModel>>(coursesUrl)
				?? new List<CourseViewModel>();
			
			string competencesUrl = $"{_apiUrl}/Competence/ByTeacher/{id}";
			CompetenceModels = await httpClient.GetFromJsonAsync<List<CompetenceViewModel>>(competencesUrl)
				?? new List<CompetenceViewModel>();
		}
	}
}
