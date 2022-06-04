namespace WestcoastEducationApi.ViewModels.Courses;

public class PutCourseViewModel
{
    public int Id { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public int? Days { get; set; }
    public double? Hours { get; set; }
    public int CategoryId { get; set; }
}