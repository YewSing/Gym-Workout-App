using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWorkoutApp.DTOs.Auth;
using MyWorkoutApp.Models;
using MyWorkoutApp.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MyWorkoutApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> Register(RegisterDto dto)
        {
            if(await _context.Users.AnyAsync(u => u.Email == dto.Email)) 
                return false;

            var user = new User
            {
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                UserName = dto.Username
            };

            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex); // see exactly why insert fails
                throw;
            }
            return true;
        }

        public async Task<AuthResponseDto?> Login(LoginDto dto)
        {
            var user =  await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return null;    

            bool isValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isValid)
                return null;

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token
            };
        }

        public async Task Logout()
        {
            // Stateless JWT logout is handled on the client.
            // In a stateful system, we might invalidate a session or refresh token here.
            await Task.CompletedTask;
        }

        public async Task<MeDto?> GetMe(System.Security.Claims.ClaimsPrincipal principal)
        {
            var idClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null || !int.TryParse(idClaim.Value, out var userId)) return null;

            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            return new MeDto { Email = user.Email, Username = user.UserName };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
