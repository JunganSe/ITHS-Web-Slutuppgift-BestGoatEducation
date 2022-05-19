using System.ComponentModel.DataAnnotations;

namespace WestcoastEducationApi.Models;

public class Competence
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }



    public ICollection<Teacher_Competence> Teacher_Competences { get; set; } = new List<Teacher_Competence>();
}