namespace WestcoastEducationApi.Models;

public class Teacher_Competence
{
    public Teacher? Teacher { get; set; }
    public int TeacherId { get; set; }
    public Competence? Competence { get; set; }
    public int CompetenceId { get; set; }
}