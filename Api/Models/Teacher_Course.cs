using System.ComponentModel.DataAnnotations.Schema;

namespace WestcoastEducationApi.Models;

public class Teacher_Course
{
    [ForeignKey("TeacherId")]
    public AppUser? Teacher { get; set; }
    public string? TeacherId { get; set; }
    
    public Course? Course { get; set; }
    public int CourseId { get; set; }
}