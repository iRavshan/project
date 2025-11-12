using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Dtos.ClassroomDtos;
using UserService.Infrastructure.Interfaces;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Teacher, Admin, SuperAdmin")]
public class ClassroomsController(
    IClassroomService classroomService
) : ControllerBase
{
    private Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    [HttpPost]
    public async Task<IActionResult> CreateClassroom(ClassroomCreateDto dto)
    {
        var classroom = await classroomService.CreateClassroomAsync(CurrentUserId, dto);
        return Ok(classroom);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetClassroom(Guid id)
    {
        var classroom = await classroomService.GetClassroomByIdAsync(id);
        return Ok(classroom);
    }

    [HttpGet("{CurrentUserId}/classrooms")]
    public async Task<IActionResult> GetMyClassrooms()
    {
        var classrooms = await classroomService.GetClassroomsByTeacherAsync(CurrentUserId);
        return Ok(classrooms);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClassroom(Guid id, ClassroomUpdateDto dto)
    {
        var updatedClassroom = await classroomService.UpdateClassroomAsync(CurrentUserId, id, dto);
        return Ok(updatedClassroom);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClassroom(Guid id)
    {
        await classroomService.DeleteClassroomAsync(CurrentUserId, id);
        return NoContent();
    }
    
    [HttpPost("{classroomId}/students")]
    public async Task<IActionResult> EnrollStudent(Guid classroomId, [FromBody] EnrollStudentDto dto)
    {
        await classroomService.EnrollStudentAsync(CurrentUserId, classroomId, dto.StudentId);
        var students = await classroomService.GetStudentsInClassroomAsync(CurrentUserId, classroomId);
        return Ok(students);
    }

    [HttpDelete("{classroomId}/students/{studentId}")]
    public async Task<IActionResult> RemoveStudent(Guid classroomId, Guid studentId)
    {
        await classroomService.RemoveStudentAsync(CurrentUserId, classroomId, studentId);
        return Ok(new { message = "Student removed successfully" });
    }
    
    [HttpGet("{classroomId}/students")]
    public async Task<IActionResult> GetStudentsInClassroom(Guid classroomId)
    {
        var students = await classroomService.GetStudentsInClassroomAsync(CurrentUserId, classroomId);
        return Ok(students);
    }
}