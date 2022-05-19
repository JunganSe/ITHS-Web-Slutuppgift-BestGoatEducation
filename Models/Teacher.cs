using System.ComponentModel.DataAnnotations;

namespace WestcoastEducationApi.Models;

public class Teacher
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
    
    
    
    public ICollection<Teacher_Competence> Teacher_Competences { get; set; } = new List<Teacher_Competence>();
}