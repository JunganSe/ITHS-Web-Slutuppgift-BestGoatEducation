using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Interfaces;

public interface ICompetenceRepository
{
    public Task<List<Competence>> GetAllCompetencesAsync();
    public Task<Competence?> GetCompetenceAsync(int id);
    public Task<List<Competence>> GetCompetencesByTeacherAsync(string teacherId);
    public Task CreateCompetenceAsync(Competence competence);
    public Task<bool> SaveAllAsync();
}