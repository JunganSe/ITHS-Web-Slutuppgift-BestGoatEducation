using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Data;

public static class Seed
{
    private static Context? _context;
    private static readonly JsonSerializerOptions _jsonOptions;
    private static List<AppUser>? _students;
    private static List<AppUser>? _teachers;

    static Seed()
    {
        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
    }



    public static async Task SeedDataAsync(WebApplication app, bool deleteExistingDb)
    {
        var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        _context = services.GetRequiredService<Context>();

        if (deleteExistingDb)
        {
            await _context.Database.EnsureDeletedAsync();
        }
        await _context.Database.MigrateAsync();

        await SeedAddressesAsync();
        await SeedCategoriesAsync();
        await SeedCompetencesAsync();
        await SeedRolesAsync();
        await _context.SaveChangesAsync();

        await SeedCoursesAsync();
        await SeedStudentsAsync();
        await SeedTeachersAsync();
        await _context.SaveChangesAsync();

        await SeedAssignRolesAsync();
        var courses = await _context.Courses.ToListAsync();
        var competences = await _context.Competences.ToListAsync();
        await SeedStudentCoursesAsync(courses);
        await SeedTeacherCoursesAsync(courses);
        await SeedTeacherCompetencesAsync(competences);
        await _context.SaveChangesAsync();
    }



    private static async Task SeedAddressesAsync()
    {
        if (await _context!.Addresses.AnyAsync())
            return;

        string data = await File.ReadAllTextAsync("Data/Seed/Addresses.json");
        var addresses = JsonSerializer.Deserialize<List<Address>>(data, _jsonOptions);

        await _context.Addresses.AddRangeAsync(addresses!);
    }

    private static async Task SeedCategoriesAsync()
    {
        if (await _context!.Categories.AnyAsync())
            return;

        string data = await File.ReadAllTextAsync("Data/Seed/Categories.json");
        var categories = JsonSerializer.Deserialize<List<Category>>(data, _jsonOptions);

        await _context.Categories.AddRangeAsync(categories!);
    }

    private static async Task SeedCompetencesAsync()
    {
        if (await _context!.Competences.AnyAsync())
            return;

        string data = await File.ReadAllTextAsync("Data/Seed/Competences.json");
        var competences = JsonSerializer.Deserialize<List<Competence>>(data, _jsonOptions);

        await _context.Competences.AddRangeAsync(competences!);
    }
    
    private static async Task SeedRolesAsync()
    {
        var roles = new List<IdentityRole>()
        {
            new IdentityRole() {Name = "Student"},
            new IdentityRole() {Name = "Teacher"}
        };
        
        await _context!.Roles.AddRangeAsync(roles);
    }

    private static async Task SeedCoursesAsync()
    {
        if (await _context!.Courses.AnyAsync())
            return;

        string data = await File.ReadAllTextAsync("Data/Seed/Courses.json");
        var courses = JsonSerializer.Deserialize<List<Course>>(data, _jsonOptions);

        await _context.Courses.AddRangeAsync(courses!);
    }

    private static async Task SeedStudentsAsync()
    {
        if (await _context!.AppUsers.AnyAsync())
            return;

        string data = await File.ReadAllTextAsync("Data/Seed/Students.json");
        _students = JsonSerializer.Deserialize<List<AppUser>>(data, _jsonOptions);

        await _context.AppUsers.AddRangeAsync(_students!);
    }

    private static async Task SeedTeachersAsync()
    {
        if (await _context!.AppUsers.AnyAsync())
            return;

        string data = await File.ReadAllTextAsync("Data/Seed/Teachers.json");
        _teachers = JsonSerializer.Deserialize<List<AppUser>>(data, _jsonOptions);

        await _context.AppUsers.AddRangeAsync(_teachers!);
    }
    
    private static async Task SeedAssignRolesAsync()
    {
        var userRoles =  new List<IdentityUserRole<string>>();
        
        var studentRole = await _context!.Roles.FirstAsync(r => r.Name == "Student");
        foreach (var student in _students!)
        {
            userRoles.Add(new IdentityUserRole<string>() { UserId = student.Id, RoleId = studentRole.Id });
        }
        
        var teacherRole = await _context!.Roles.FirstAsync(r => r.Name == "Teacher");
        foreach (var teacher in _teachers!)
        {
            userRoles.Add(new IdentityUserRole<string>() { UserId = teacher.Id, RoleId = teacherRole.Id });
        }
        
        await _context!.UserRoles.AddRangeAsync(userRoles);
    }

    private static async Task SeedStudentCoursesAsync(List<Course> courses)
    {
        if (await _context!.Student_Courses.AnyAsync())
            return;

        var studentCourses = new List<Student_Course>();
        var grades = new List<string>() { "IG", "G", "VG" };
        var random = new Random();

        for (int i = 0; i < _students!.Count * 3; i++)
        {
            int studentIndex = random.Next(0, _students.Count);
            string studentId = _students[studentIndex].Id;
            int courseId = random.Next(0, courses.Count) + 1;
            if (studentCourses.Exists(sc => sc.StudentId == studentId && sc.CourseId == courseId))
            {
                i--;
                continue;
            }

            var sc = new Student_Course() { StudentId = studentId, CourseId = courseId };
            sc.IsStarted = RandomBool();
            sc.IsCompleted = sc.IsStarted && RandomBool();
            sc.Grade = sc.IsCompleted ? grades[random.Next(0, 3)] : null;
            studentCourses.Add(sc);
        }

        await _context.Student_Courses.AddRangeAsync(studentCourses);
    }

    private static async Task SeedTeacherCoursesAsync(List<Course> courses)
    {
        if (await _context!.Teacher_Courses.AnyAsync())
            return;

        var teacherCourses = new List<Teacher_Course>();
        var random = new Random();

        for (int i = 0; i < _teachers!.Count * 3; i++)
        {
            int teacherIndex = random.Next(0, _teachers.Count);
            string teacherId = _teachers[teacherIndex].Id;
            int courseId = random.Next(0, courses.Count) + 1;
            if (teacherCourses.Exists(tc => tc.TeacherId == teacherId && tc.CourseId == courseId))
            {
                i--;
                continue;
            }

            teacherCourses.Add(new Teacher_Course() { TeacherId = teacherId, CourseId = courseId });
        }

        await _context.Teacher_Courses.AddRangeAsync(teacherCourses);
    }

    private static async Task SeedTeacherCompetencesAsync(List<Competence> competences)
    {
        if (await _context!.Teacher_Competences.AnyAsync())
            return;

        var teacherCompetences = new List<Teacher_Competence>();
        var random = new Random();

        for (int i = 0; i < _teachers!.Count * 3; i++)
        {
            int teacherIndex = random.Next(0, _teachers.Count);
            string teacherId = _teachers[teacherIndex].Id;
            int competenceId = random.Next(0, competences.Count) + 1;
            if (teacherCompetences.Exists(tc => tc.TeacherId == teacherId && tc.CompetenceId == competenceId))
            {
                i--;
                continue;
            }

            teacherCompetences.Add(new Teacher_Competence() { TeacherId = teacherId, CompetenceId = competenceId });
        }

        await _context.Teacher_Competences.AddRangeAsync(teacherCompetences);
    }



    private static bool RandomBool()
    {
        var rand = new Random();
        return rand.Next(0, 2) == 0;
    }
}