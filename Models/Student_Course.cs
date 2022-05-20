using System.ComponentModel.DataAnnotations.Schema;

namespace WestcoastEducationApi.Models;

public class Student_Course
{
    [ForeignKey("StudentId")]
    public AppUser? Student { get; set; }
    public string? StudentId { get; set; }
    
    public Course? Course { get; set; }
    public int CourseId { get; set; }
    
    
    
    public bool IsStarted { get; set; } = false;
    public bool IsCompleted { get; set; } = false;
    public string? Grade { get; set; }
}