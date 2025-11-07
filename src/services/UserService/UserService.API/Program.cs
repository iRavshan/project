using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Contexts;
using UserService.Application.Interfaces;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Repositories;
using UserService.Infrastructure.Interfaces;
using UserService.Infrastructure.Services;
using UserService.Infrastructure.Middlewares;
using UserService.Application.Interfaces;
using IUserService = UserService.Application.Interfaces.IUserService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("UserService.Infrastructure")));

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddScoped<IClassroomRepository, ClassroomRepository>();
builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IUserService, UserService.Infrastructure.Services.UserService>();

builder.Services.AddScoped<IClassroomService, ClassroomService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
