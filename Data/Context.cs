using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Data;

public class Context : DbContext
{
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<City> Cities => Set<City>();
    public DbSet<Competence> Competences => Set<Competence>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<Course> Courses => Set<Course>();
    public DbSet<PostalCode> PostalCodes => Set<PostalCode>();
    public DbSet<Street> Streets => Set<Street>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Student_Course> Student_Courses => Set<Student_Course>();
    public DbSet<Teacher_Competence> Teacher_Competences => Set<Teacher_Competence>();
    
    public Context(DbContextOptions options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student_Course>()
            .HasKey(sc => new { sc.StudentId, sc.CourseId });
        
        modelBuilder.Entity<Teacher_Competence>()
            .HasKey(tc => new { tc.TeacherId, tc.CompetenceId });
    }
}