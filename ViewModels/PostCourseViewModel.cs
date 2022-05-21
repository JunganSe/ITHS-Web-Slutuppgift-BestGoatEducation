namespace WestcoastEducationApi.ViewModels;

public class PostCourseViewModel
{
    public int Code { get; set; }
    public string? Name { get; set; }
    public string? Summary { get; set; }
    public string? Description { get; set; }
    public int? Days { get; set; }
    public double? Hours { get; set; }
    public int CategoryId { get; set; }
}