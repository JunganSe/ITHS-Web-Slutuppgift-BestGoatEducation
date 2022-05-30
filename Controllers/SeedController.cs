using System.Text.Json;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Models;

namespace WestcoastEducationApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeedController : ControllerBase
{
    private readonly Context _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JsonSerializerOptions _jsonOptions;

    public SeedController(Context context, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
        _jsonOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };
    }



    // POST: api/Seed
    [HttpPost]
    public async Task SeedDataAsync()
    {
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.MigrateAsync();

        await SeedAddressesAsync();
        await SeedCategoriesAsync();
        await SeedCompetencesAsync();
        await _context.SaveChangesAsync();

        var addresses = _context.Addresses.ToList();
        await SeedCoursesAsync();
        await SeedStudentsAsync(addresses);
        await SeedTeachersAsync(addresses);
        await _context.SaveChangesAsync();

        var courses = await _context.Courses.ToListAsync();
        var competences = await _context.Competences.ToListAsync();
        await SeedStudentCoursesAsync(courses);
        await SeedTeacherCoursesAsync(courses);
        await SeedTeacherCompetencesAsync(competences);
        await _context.SaveChangesAsync();
    }



    private async Task SeedAddressesAsync()
    {
        string data = await System.IO.File.ReadAllTextAsync("./Data/Seed/Addresses.json");
        var addresses = JsonSerializer.Deserialize<List<Address>>(data, _jsonOptions);

        await _context.Addresses.AddRangeAsync(addresses!);
    }

    private async Task SeedCategoriesAsync()
    {
        string data = await System.IO.File.ReadAllTextAsync("./Data/Seed/Categories.json");
        var categories = JsonSerializer.Deserialize<List<Category>>(data, _jsonOptions);

        await _context.Categories.AddRangeAsync(categories!);
    }

    private async Task SeedCompetencesAsync()
    {
        string data = await System.IO.File.ReadAllTextAsync("./Data/Seed/Competences.json");
        var competences = JsonSerializer.Deserialize<List<Competence>>(data, _jsonOptions);

        await _context.Competences.AddRangeAsync(competences!);
    }

    private async Task SeedCoursesAsync()
    {
        string data = await System.IO.File.ReadAllTextAsync("./Data/Seed/Courses.json");
        var courses = JsonSerializer.Deserialize<List<Course>>(data, _jsonOptions);

        await _context.Courses.AddRangeAsync(courses!);
    }

    private async Task SeedStudentsAsync(List<Address> addresses)
    {
        string data = await System.IO.File.ReadAllTextAsync("./Data/Seed/Students.json");
        var students = JsonSerializer.Deserialize<List<AppUser>>(data, _jsonOptions);
        var random = new Random();

        var role = new IdentityRole { Name = "Student" };
        await _roleManager.CreateAsync(role);
        foreach (var student in students!)
        {
            student.AddressId = addresses[random.Next(0, addresses.Count)].Id;
            student.UserName = student.Email;
            await _userManager.CreateAsync(student);
            await _userManager.AddToRoleAsync(student, role.Name);
        }
    }

    private async Task SeedTeachersAsync(List<Address> addresses)
    {
        string data = await System.IO.File.ReadAllTextAsync("./Data/Seed/Teachers.json");
        var teachers = JsonSerializer.Deserialize<List<AppUser>>(data, _jsonOptions);
        var random = new Random();

        var role = new IdentityRole { Name = "Teacher" };
        await _roleManager.CreateAsync(role);
        foreach (var teacher in teachers!)
        {
            teacher.AddressId = addresses[random.Next(0, addresses.Count)].Id;
            teacher.UserName = teacher.Email;
            await _userManager.CreateAsync(teacher);
            await _userManager.AddToRoleAsync(teacher, role.Name);
        }
    }

    private async Task SeedStudentCoursesAsync(List<Course> courses)
    {
        var students = await _userManager.GetUsersInRoleAsync("Student");
        var studentCourses = new List<Student_Course>();
        var grades = new List<string>() { "IG", "G", "VG" };
        var random = new Random();

        for (int i = 0; i < students.Count * 3; i++)
        {
            int studentIndex = random.Next(0, students.Count);
            string studentId = students[studentIndex].Id;
            int courseId = random.Next(0, courses.Count) + 1;
            if (studentCourses.Exists(sc => sc.StudentId == studentId && sc.CourseId == courseId))
            {
                i--;
                continue;
            }

            var sc = new Student_Course() { StudentId = studentId, CourseId = courseId };
            sc.IsStarted = RandomBool(3);
            sc.IsCompleted = sc.IsStarted && RandomBool();
            sc.Grade = sc.IsCompleted ? grades[random.Next(0, 3)] : null;
            studentCourses.Add(sc);
        }

        await _context.Student_Courses.AddRangeAsync(studentCourses);
    }

    private async Task SeedTeacherCoursesAsync(List<Course> courses)
    {
        var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
        var teacherCourses = new List<Teacher_Course>();
        var random = new Random();

        for (int i = 0; i < teachers.Count * 3; i++)
        {
            int teacherIndex = random.Next(0, teachers.Count);
            string teacherId = teachers[teacherIndex].Id;
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

    private async Task SeedTeacherCompetencesAsync(List<Competence> competences)
    {
        var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
        var teacherCompetences = new List<Teacher_Competence>();
        var random = new Random();

        for (int i = 0; i < teachers.Count * 3; i++)
        {
            int teacherIndex = random.Next(0, teachers.Count);
            string teacherId = teachers[teacherIndex].Id;
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



    private bool RandomBool(int trueChance = 1)
    {
        return new Random().Next(0, 1 + trueChance) != 0;
    }
}