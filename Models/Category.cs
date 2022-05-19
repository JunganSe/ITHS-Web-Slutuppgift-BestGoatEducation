using System.ComponentModel.DataAnnotations;

namespace WestcoastEducationApi.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
    
    
    
    public ICollection<Course> Courses { get; set; } = new List<Course>();
}