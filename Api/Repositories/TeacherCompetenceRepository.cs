using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Repositories;

public class TeacherCompetenceRepository : ITeacherCompetenceRepository
{
    private readonly Context _context;

    public TeacherCompetenceRepository(Context context)
    {
        _context = context;
    }



    public async Task<Teacher_Competence?> GetTeacherCompetenceAsync(string teacherId, int courseId)
    {
        return await _context.Teacher_Competences
            .FirstOrDefaultAsync(sc => sc.TeacherId == teacherId && sc.CompetenceId == courseId);
    }



    public async Task CreateTeacherCompetenceAsync(Teacher_Competence teacherCompetence)
    {
        await _context.Teacher_Competences.AddAsync(teacherCompetence);
    }



    public void DeleteTeacherCompetence(Teacher_Competence teacherCompetence)
    {
        _context.Teacher_Competences.Remove(teacherCompetence);
    }



    public async Task<bool> SaveAllAsync()
    {
        return (await _context.SaveChangesAsync() > 0);
    }
}