using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWorkoutApp.DTOs.Auth;
using MyWorkoutApp.Services;

namespace MyWorkoutApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService; 
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(RegisterDto dto)
        {
            var result = await _authService.Register(dto);

            if (!result)
                return BadRequest(new { message = "User already exists." });

            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
        {
            var result = await _authService.Login(dto);

            if(result == null)
                return Unauthorized(new { message = "Invalid email or password." });

            return Ok(result);
        }

        [Authorize]
        [HttpGet("Me")]
        public async Task<IActionResult> Me()
        {
            var result = await _authService.GetMe(User);
            if (result == null) return Unauthorized();
            return Ok(result);
        }

        [Authorize]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _authService.Logout();
            return Ok(new { message = "Logged out successfully" });
        }
    }
}
