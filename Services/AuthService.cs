using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using app.Data;
using app.DTOs;
using app.Models;
using app.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace app.Services
{
    public class AuthService(AppDbContext context, IConfiguration configuration):IAuthService
    {
        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
        {
            var emailExists = await context.Users.AnyAsync(u => u.Email == dto.Email);
            if(emailExists) return null;
            var user = new User
            {
                Fullname = dto.Fullname,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role="User"
            };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role
            };
        }
        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if(user == null)
            {
                return null;
            }

            var passwordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!passwordValid)
            {
                return null;
            }

            var token = GenerateJwtToken(user);
            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                Role = user.Role
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!)
            );
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiresInMinutes = Convert.ToDouble(configuration["Jwt:ExpiresInMinutes"]);
            var token = new JwtSecurityToken(
                issuer:configuration["Jwt:Issuer"],
                audience:configuration["Jwt:Audience"],
                claims:claims,
                expires:DateTime.UtcNow.AddMinutes(expiresInMinutes),
                signingCredentials:credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}