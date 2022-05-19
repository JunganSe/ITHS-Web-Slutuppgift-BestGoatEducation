namespace WestcoastEducationApi.Models;

public class Student_Course
{
    public Student? Student { get; set; }
    public int StudentId { get; set; }
    public Course? Course { get; set; }
    public int CourseId { get; set; }
    
    public DateTime EnrollmentDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsCompleted { get; set; }
}