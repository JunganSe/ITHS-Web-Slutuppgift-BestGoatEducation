using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Repositories;

public class CompetenceRepository : ICompetenceRepository
{
    private readonly Context _context;
    
    public CompetenceRepository(Context context)
    {
        _context = context;
    }



    public async Task<List<Competence>> GetAllCompetencesAsync()
    {
        return await _context.Competences.ToListAsync();
    }

    public async Task<Competence?> GetCompetenceAsync(int id)
    {
        return await _context.Competences.FindAsync(id);
    }

    public async Task CreateCompetenceAsync(Competence competence)
    {
        await _context.Competences.AddAsync(competence);
    }

    public async Task<List<Competence>> GetCompetencesByTeacherAsync(string teacherId)
    {
        return await _context.Teacher_Competences
            .Where(tc => tc.TeacherId == teacherId)
            .Select(tc => tc.Competence!)
            .ToListAsync();
    }



    public async Task<bool> SaveAllAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}