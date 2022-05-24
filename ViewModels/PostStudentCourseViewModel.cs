namespace WestcoastEducationApi.ViewModels;

public class PostStudentCourseViewModel
{
    public string? StudentId { get; set; }
    public int CourseId { get; set; }
    
    public bool IsStarted { get; set; }
    public bool IsCompeleted { get; set; }
    public string? Grade { get; set; }
}