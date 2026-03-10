using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ZamAiMart.Core.DTOs;
using ZamAiMart.Core.Interfaces;

namespace ZamAiMart.Infrastructure.Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _repo;
    private readonly IConfiguration _config;

    public AdminService(IAdminRepository repo, IConfiguration config)
    {
        _repo = repo;
        _config = config;
    }

    public async Task<AdminLoginResponseDto> AuthenticateAsync(AdminLoginDto dto)
    {
        var admin = await _repo.GetByUsernameAsync(dto.Username);
        if (admin is null || !BCrypt.Net.BCrypt.Verify(dto.Password, admin.PasswordHash))
        {
            return new AdminLoginResponseDto { Success = false, Message = "Invalid username or password." };
        }

        var token = GenerateJwtToken(admin.Username);
        return new AdminLoginResponseDto { Success = true, Token = token, Message = "Login successful." };
    }

    public Task<bool> ValidateTokenAsync(string token)
    {
        try
        {
            var secret = _config["Jwt:Secret"] ?? "ZamAiMart-SuperSecret-Key-2024!";
            var handler = new JwtSecurityTokenHandler();
            handler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out _);
            return Task.FromResult(true);
        }
        catch
        {
            return Task.FromResult(false);
        }
    }

    private string GenerateJwtToken(string username)
    {
        var secret = _config["Jwt:Secret"] ?? "ZamAiMart-SuperSecret-Key-2024!";
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
