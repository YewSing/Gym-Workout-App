using System.Security.Claims;
using MyWorkoutApp.DTOs.Auth;

namespace MyWorkoutApp.Services
{
    public interface IAuthService
    {
        Task<bool> Register(RegisterDto dto);
        Task<AuthResponseDto?> Login(LoginDto dto);
        Task Logout();
        Task<MeDto?> GetMe(ClaimsPrincipal user);
    }
}
