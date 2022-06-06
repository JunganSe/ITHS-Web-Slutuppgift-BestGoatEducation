namespace WestcoastEducationApi.ViewModels.StudentCourses;

public class PostStudentCourseViewModel
{
    public string? StudentId { get; set; }
    public int CourseId { get; set; }
    
    public bool IsStarted { get; set; }
    public bool IsCompleted { get; set; }
    public string? Grade { get; set; }
}