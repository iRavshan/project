using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Dtos;
using UserService.Infrastructure.Interfaces;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IResult> Register(RegisterUserDto dto)
        {
            await userService.Register(dto.UserName, dto.Email, dto.Password, dto.FirstName, dto.LastName,  dto.Phone);
            return Results.Ok();
        }

        [HttpPost("login")]
        public async Task<IResult> Login(LoginUserDto dto)
        {
            var token = await userService.Login(dto.UserName, dto.Password);
            return Results.Ok(token);
        }
    }
}
