using ZamAiMart.Core.Entities;

namespace ZamAiMart.Core.Interfaces;

public interface IAdminRepository
{
    Task<AdminUser?> GetByUsernameAsync(string username);
    Task<AdminUser> CreateAsync(AdminUser admin);
    Task<bool> ExistsAsync();
}
