namespace WestcoastEducationApi.Models;

public class Student
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public Address? Address { get; set; }
    public int AddressId { get; set; }

    public ICollection<Student_Course> Student_Courses { get; set; } = new List<Student_Course>();
}