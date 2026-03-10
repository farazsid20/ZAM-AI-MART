using ZamAiMart.Core.DTOs;

namespace ZamAiMart.Core.Interfaces;

public interface IAdminService
{
    Task<AdminLoginResponseDto> AuthenticateAsync(AdminLoginDto dto);
    Task<bool> ValidateTokenAsync(string token);
}
