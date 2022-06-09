using System.ComponentModel.DataAnnotations.Schema;

namespace WestcoastEducationApi.Models;

public class Teacher_Competence
{
    [ForeignKey("TeacherId")]
    public AppUser? Teacher { get; set; }
    public string? TeacherId { get; set; }
    public Competence? Competence { get; set; }
    public int CompetenceId { get; set; }
}