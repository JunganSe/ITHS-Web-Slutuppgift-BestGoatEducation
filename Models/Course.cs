namespace WestcoastEducationApi.Models;

public class Course
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    
    public ICollection<Student_Course> Student_Courses { get; set; } = new List<Student_Course>();
}