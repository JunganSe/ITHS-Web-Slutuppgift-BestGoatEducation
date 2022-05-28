namespace WestcoastEducationStudentApp.ViewModels;

public class PostStudentCourseViewModel
{
    public string? StudentId { get; set; }
    public int CourseId { get; set; }

    public bool IsStarted { get; set; } = false;
    public bool IsCompeleted { get; set; } = false;
    public string? Grade { get; set; } = null;
}