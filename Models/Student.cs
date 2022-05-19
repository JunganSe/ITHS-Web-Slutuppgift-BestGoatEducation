using System.ComponentModel.DataAnnotations;

namespace WestcoastEducationApi.Models;

public class Student
{
    public int Id { get; set; }

    [Required]
    public string? FirstName { get; set; }

    [Required]
    public string? LastName { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Phone]    
    public string? PhoneNumber { get; set; }
    
    public Address? Address { get; set; }
    public int AddressId { get; set; }



    public ICollection<Student_Course> Student_Courses { get; set; } = new List<Student_Course>();
}