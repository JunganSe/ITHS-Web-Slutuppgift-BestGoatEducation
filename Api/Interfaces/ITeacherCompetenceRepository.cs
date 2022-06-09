using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Interfaces;

public interface ITeacherCompetenceRepository
{
    public Task<Teacher_Competence?> GetTeacherCompetenceAsync(string teacherId, int courseId);
    public Task CreateTeacherCompetenceAsync(Teacher_Competence teacherCompetence);
    public void DeleteTeacherCompetence(Teacher_Competence teacherCompetence);
    public Task<bool> SaveAllAsync();
}