namespace WestcoastEducationApi.Models;

public class Teacher
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public Address? Address { get; set; }
    public int AddressId { get; set; }
    
    public ICollection<Teacher_Competence> Teacher_Competences { get; set; } = new List<Teacher_Competence>();
}