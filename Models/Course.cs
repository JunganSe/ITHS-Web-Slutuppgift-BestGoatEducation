using System.ComponentModel.DataAnnotations;

namespace WestcoastEducationApi.Models;

public class Course
{
    public int Id { get; set; }
    
    [Required]
    public int Code { get; set; }

    [Required]
    public string? Name { get; set; }
    
    public string? Summary { get; set; }
    
    public string? Description { get; set; }
    
    public int? Days { get; set; }
    
    public TimeSpan? Duration { get; set; }

    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    
    
    
    public ICollection<Student_Course> Student_Courses { get; set; } = new List<Student_Course>();
    
    public ICollection<Teacher_Course> Teacher_Courses { get; set; } = new List<Teacher_Course>();
}