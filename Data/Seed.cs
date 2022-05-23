using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Data;

public static class Seed
{
    private static Context? _context;
    private static readonly JsonSerializerOptions _jsonOptions;

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
        await _context.SaveChangesAsync();

        await SeedCoursesAsync();
        await SeedAppUsersAsync();
        await _context.SaveChangesAsync();

        var appUsers = await _context.AppUsers.ToListAsync();
        await SeedStudentCoursesAsync(appUsers);
        await SeedTeacherCoursesAsync(appUsers);
        await SeedTeacherCompetencesAsync(appUsers);
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

    private static async Task SeedCoursesAsync()
    {
        if (await _context!.Courses.AnyAsync())
            return;

        string data = await File.ReadAllTextAsync("Data/Seed/Courses.json");
        var courses = JsonSerializer.Deserialize<List<Course>>(data, _jsonOptions);

        await _context.Courses.AddRangeAsync(courses!);
    }

    private static async Task SeedAppUsersAsync()
    {
        if (await _context!.AppUsers.AnyAsync())
            return;

        string data = await File.ReadAllTextAsync("Data/Seed/AppUsers.json");
        var appUsers = JsonSerializer.Deserialize<List<AppUser>>(data, _jsonOptions);

        await _context.AppUsers.AddRangeAsync(appUsers!);
    }

    private static async Task SeedStudentCoursesAsync(List<AppUser> students)
    {
        if (await _context!.Student_Courses.AnyAsync())
            return;

        var studentCourses = new List<Student_Course>()
        {
            new Student_Course()
            {
                StudentId = students[0].Id,
                CourseId = 1,
                IsStarted = true,
                IsCompleted = false,
                Grade = null
            },
            new Student_Course()
            {
                StudentId = students[1].Id,
                CourseId = 2,
                IsStarted = true,
                IsCompleted = false,
                Grade = null
            },
            new Student_Course()
            {
                StudentId = students[1].Id,
                CourseId = 5,
                IsStarted = true,
                IsCompleted = true,
                Grade = "G"
            },
            new Student_Course()
            {
                StudentId = students[2].Id,
                CourseId = 6,
                IsStarted = true,
                IsCompleted = false,
                Grade = null
            },
            new Student_Course()
            {
                StudentId = students[3].Id,
                CourseId = 1,
                IsStarted = true,
                IsCompleted = true,
                Grade = "VG"
            },
            new Student_Course()
            {
                StudentId = students[3].Id,
                CourseId = 3,
                IsStarted = true,
                IsCompleted = true,
                Grade = "VG"
            },
            new Student_Course()
            {
                StudentId = students[4].Id,
                CourseId = 4,
                IsStarted = false,
                IsCompleted = false,
                Grade = null
            },
            new Student_Course()
            {
                StudentId = students[5].Id,
                CourseId = 1,
                IsStarted = true,
                IsCompleted = true,
                Grade = "G"
            }
        };

        await _context.Student_Courses.AddRangeAsync(studentCourses);
    }

    private static async Task SeedTeacherCoursesAsync(List<AppUser> teachers)
    {
        if (await _context!.Teacher_Courses.AnyAsync())
            return;

        var teacherCourses = new List<Teacher_Course>()
        {
            new Teacher_Course()
            {
                TeacherId = teachers[7].Id,
                CourseId = 1
            },
            new Teacher_Course()
            {
                TeacherId = teachers[7].Id,
                CourseId = 2
            },
            new Teacher_Course()
            {
                TeacherId = teachers[7].Id,
                CourseId = 3
            },
            new Teacher_Course()
            {
                TeacherId = teachers[7].Id,
                CourseId = 4
            },
            new Teacher_Course()
            {
                TeacherId = teachers[6].Id,
                CourseId = 2
            },
            new Teacher_Course()
            {
                TeacherId = teachers[6].Id,
                CourseId = 5
            },
            new Teacher_Course()
            {
                TeacherId = teachers[6].Id,
                CourseId = 6
            },
            new Teacher_Course()
            {
                TeacherId = teachers[5].Id,
                CourseId = 1
            },
            new Teacher_Course()
            {
                TeacherId = teachers[5].Id,
                CourseId = 3
            },
            new Teacher_Course()
            {
                TeacherId = teachers[5].Id,
                CourseId = 7
            },
        };

        await _context.Teacher_Courses.AddRangeAsync(teacherCourses);
    }

    private static async Task SeedTeacherCompetencesAsync(List<AppUser> teachers)
    {
        if (await _context!.Teacher_Competences.AnyAsync())
            return;
            
        var teacherCompetences = new List<Teacher_Competence>()
        {
            new Teacher_Competence()
            {
                TeacherId = teachers[7].Id,
                CompetenceId = 1
            },
            new Teacher_Competence()
            {
                TeacherId = teachers[7].Id,
                CompetenceId = 2
            },
            new Teacher_Competence()
            {
                TeacherId = teachers[7].Id,
                CompetenceId = 3
            },
            new Teacher_Competence()
            {
                TeacherId = teachers[7].Id,
                CompetenceId = 4
            },
            new Teacher_Competence()
            {
                TeacherId = teachers[7].Id,
                CompetenceId = 5
            },
            new Teacher_Competence()
            {
                TeacherId = teachers[6].Id,
                CompetenceId = 2
            },
            new Teacher_Competence()
            {
                TeacherId = teachers[6].Id,
                CompetenceId = 4
            },
            new Teacher_Competence()
            {
                TeacherId = teachers[6].Id,
                CompetenceId = 5
            },
            new Teacher_Competence()
            {
                TeacherId = teachers[6].Id,
                CompetenceId = 6
            },
            new Teacher_Competence()
            {
                TeacherId = teachers[5].Id,
                CompetenceId = 1
            },
            new Teacher_Competence()
            {
                TeacherId = teachers[5].Id,
                CompetenceId = 3
            },
            new Teacher_Competence()
            {
                TeacherId = teachers[5].Id,
                CompetenceId = 6
            }
        };

        await _context.Teacher_Competences.AddRangeAsync(teacherCompetences);
    }
}