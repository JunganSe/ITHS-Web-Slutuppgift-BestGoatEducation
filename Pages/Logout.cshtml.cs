using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WestcoastEducationStudentApp.Pages;

public class Logout : PageModel
{
    public Logout()
    {
    }

    public void OnGet()
    {
    }
    
    public void OnPost()
    {
        HttpContext.Session.Clear();
    }
}
