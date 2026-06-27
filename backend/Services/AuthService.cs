using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWorkoutApp.DTOs.Auth;
using MyWorkoutApp.Models;
using MyWorkoutApp.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
            var email = dto.Email.Trim();

            if(await _context.Users.AnyAsync(u => u.Email == email))
                return false;

            var user = new User
            {
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                UserName = dto.Username.Trim()
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
            var refreshToken = await CreateRefreshTokenAsync(user.UserId);

            return new AuthResponseDto
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }

        public async Task<AuthResponseDto?> Refresh(RefreshRequestDto dto)
        {
            var hash = HashToken(dto.RefreshToken);

            var existing = await _context.RefreshTokens
                .Include(rt => rt.User)
                .FirstOrDefaultAsync(rt => rt.TokenHash == hash);

            if (existing == null || existing.RevokedAt != null || existing.ExpiresAt <= DateTime.UtcNow)
                return null;

            // Rotation: the presented token is single-use, so revoke it before issuing a new pair.
            existing.RevokedAt = DateTime.UtcNow;

            var newAccessToken = GenerateJwtToken(existing.User);
            var newRefreshToken = await CreateRefreshTokenAsync(existing.UserId);

            await _context.SaveChangesAsync();

            return new AuthResponseDto
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }

        public async Task Logout(ClaimsPrincipal principal)
        {
            var idClaim = principal.FindFirst(ClaimTypes.NameIdentifier);
            if (idClaim == null || !int.TryParse(idClaim.Value, out var userId)) return;

            var activeTokens = await _context.RefreshTokens
                .Where(rt => rt.UserId == userId && rt.RevokedAt == null)
                .ToListAsync();

            foreach (var t in activeTokens)
                t.RevokedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
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

            var accessTokenMinutes = _configuration.GetValue("Jwt:AccessTokenMinutes", 15);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(accessTokenMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRawRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }

        private static string HashToken(string rawToken)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawToken));
            return Convert.ToHexString(bytes);
        }

        private async Task<string> CreateRefreshTokenAsync(int userId)
        {
            var raw = GenerateRawRefreshToken();
            var refreshTokenDays = _configuration.GetValue("Jwt:RefreshTokenDays", 30);

            _context.RefreshTokens.Add(new RefreshToken
            {
                UserId = userId,
                TokenHash = HashToken(raw),
                ExpiresAt = DateTime.UtcNow.AddDays(refreshTokenDays)
            });
            await _context.SaveChangesAsync();

            return raw;
        }
    }
}
