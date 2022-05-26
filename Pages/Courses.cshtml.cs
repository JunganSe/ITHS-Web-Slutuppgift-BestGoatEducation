using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace WestcoastEducationStudentApp.Pages
{
    public class Courses : PageModel
    {
        private readonly ILogger<Courses> _logger;

        public Courses(ILogger<Courses> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }
    }
}
