using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Contexts;
using UserService.Domain.Dtos.ClassroomDtos;
using UserService.Domain.Entities;
using UserService.Domain.Mappers;
using UserService.Infrastructure.Interfaces;

namespace UserService.Infrastructure.Services;

public class ClassroomService(
        AppDbContext context
    ) : IClassroomService
    {
        public async Task<ClassroomDto> CreateClassroomAsync(Guid teacherId, ClassroomCreateDto dto)
        {
            if (await context.Classrooms.AnyAsync(c => c.Code == dto.Code))
                throw new Exception($"Classroom with code '{dto.Code}' already exists.");
            
            var teacher = context.Users.FindAsync(teacherId).Result;
            if (teacher == null) throw new Exception("Teacher not found");

            var classroom = new Classroom
            {
                Name = dto.Name,
                Code = dto.Code,
                Description = dto.Description,
                TeacherId = teacherId,
                Teacher = teacher
            };

            await context.Classrooms.AddAsync(classroom);
            await context.SaveChangesAsync();

            await context.Entry(classroom).Reference(c => c.Teacher).LoadAsync();

            return classroom.ToDto();
        }

        public async Task<ClassroomDto> GetClassroomByIdAsync(Guid classroomId)
        {
            var classroom = await context.Classrooms
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                    .ThenInclude(cu => cu.User)
                .FirstOrDefaultAsync(c => c.Id == classroomId);

            if (classroom == null) throw new Exception("Classroom not found");

            return classroom.ToDto();
        }

        public async Task<List<ClassroomDto>> GetClassroomsByTeacherAsync(Guid teacherId)
        {
            var classrooms = await context.Classrooms
                .Where(c => c.TeacherId == teacherId)
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                    .ThenInclude(cu => cu.User)
                .ToListAsync();

            return classrooms.Select(c => c.ToDto()).ToList();
        }

        public async Task<ClassroomDto> UpdateClassroomAsync(Guid teacherId, Guid classroomId, ClassroomUpdateDto dto)
        {
            var classroom = await context.Classrooms
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                    .ThenInclude(cu => cu.User)
                .FirstOrDefaultAsync(c => c.Id == classroomId);

            if (classroom == null) throw new Exception("Classroom not found");
            if (classroom.TeacherId != teacherId)
                throw new Exception("You are not authorized to update this classroom");

            classroom.UpdateFromDto(dto);

            context.Classrooms.Update(classroom);
            await context.SaveChangesAsync();

            return classroom.ToDto();
        }

        public async Task DeleteClassroomAsync(Guid teacherId, Guid classroomId)
        {
            var classroom = await context.Classrooms
                .FirstOrDefaultAsync(c => c.Id == classroomId);

            if (classroom == null) throw new Exception("Classroom not found");
            if (classroom.TeacherId != teacherId)
                throw new Exception("You are not authorized to delete this classroom");

            context.Classrooms.Remove(classroom);
            await context.SaveChangesAsync();
        }
        
        public async Task EnrollStudentAsync(Guid teacherId, Guid classroomId, Guid studentId)
        {
            var classroom = await context.Classrooms
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == classroomId);

            if (classroom == null) throw new Exception("Classroom not found");
            if (classroom.TeacherId != teacherId)
                throw new Exception("You are not authorized to manage this classroom");

            if (classroom.Students.Any(s => s.UserId == studentId))
                throw new Exception("Student is already enrolled");

            var student = await context.Users.FindAsync(studentId);
            if (student == null) throw new Exception("Student not found");

            classroom.Students.Add(new ClassroomUser
            {
                UserId = studentId, 
                ClassroomId = classroomId,
                User = student,
                Classroom = classroom
            });

            await context.SaveChangesAsync();
        }

        public async Task RemoveStudentAsync(Guid teacherId, Guid classroomId, Guid studentId)
        {
            var classroomUser = await context.ClassroomUsers
                .FirstOrDefaultAsync(cu => cu.ClassroomId == classroomId && cu.UserId == studentId);

            if (classroomUser == null) throw new Exception("Student is not enrolled in this classroom");

            var classroom = await context.Classrooms.FindAsync(classroomId);
            if (classroom == null) throw new Exception("Classroom not found");
            if (classroom.TeacherId != teacherId)
                throw new Exception("You are not authorized to manage this classroom");

            context.ClassroomUsers.Remove(classroomUser);
            await context.SaveChangesAsync();
        }
        
        public async Task<List<StudentDto>> GetStudentsInClassroomAsync(Guid teacherId, Guid classroomId)
        {
            var classroom = await context.Classrooms
                .Include(c => c.Students)
                .ThenInclude(cu => cu.User)
                .FirstOrDefaultAsync(c => c.Id == classroomId);

            if (classroom == null) throw new Exception("Classroom not found");
            if (classroom.TeacherId != teacherId)
                throw new Exception("You are not authorized to view students in this classroom");

            return classroom.Students
                .Select(s => new StudentDto(
                    s.User.Id,
                    s.User.UserName,
                    s.User.Email,
                    s.User.FirstName,
                    s.User.LastName,
                    s.User.Phone
                ))
                .ToList();
        }
    }
