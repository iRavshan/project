using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Domain.Dtos;
using UserService.Infrastructure.Interfaces;

namespace UserService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        
        [HttpPost("register")]
        public async Task<IResult> Register(RegisterUserDto dto)
        {
            await _userService.Register(dto.userName, dto.email, dto.password);
            return Results.Ok();
        }

        [HttpPost("login")]
        public async Task<IResult> Login(LoginUserDto dto)
        {
            var token = await _userService.Login(dto.userName, dto.password);
            return Results.Ok(token);
        }
    }
}
