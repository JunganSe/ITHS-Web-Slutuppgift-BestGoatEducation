using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace WestcoastEducationApi.Models;

public class AppUser : IdentityUser
{
    // Ärvda: Id, Email, PhoneNumber

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    public Address? Address { get; set; }
    public int AddressId { get; set; }



    public ICollection<Student_Course> Student_Courses { get; set; } = new List<Student_Course>();
    
    public ICollection<Teacher_Course> Teacher_Courses { get; set; } = new List<Teacher_Course>();
    
    public ICollection<Teacher_Competence> Teacher_Competences { get; set; } = new List<Teacher_Competence>();
}