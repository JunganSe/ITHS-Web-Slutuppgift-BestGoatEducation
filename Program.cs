using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WestcoastEducationApi.Data;
using WestcoastEducationApi.Helpers;
using WestcoastEducationApi.Interfaces;
using WestcoastEducationApi.Models;
using WestcoastEducationApi.Repositories;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddDbContext<Context>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"))
);
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<Context>();

builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAppUserRepository, AppUserRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICompetenceRepository, CompetenceRepository>();
builder.Services.AddScoped<ICourseRepository, CourseRepository>();
builder.Services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
builder.Services.AddScoped<ITeacherCourseRepository, TeacherCourseRepository>();
builder.Services.AddScoped<ITeacherCompetenceRepository, TeacherCompetenceRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();



await new Seed(app).SeedDataAsync(false);



app.Run();
