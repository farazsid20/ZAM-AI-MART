using Microsoft.EntityFrameworkCore;
using ZamAiMart.Core.Entities;
using ZamAiMart.Core.Interfaces;
using ZamAiMart.Infrastructure.Data;

namespace ZamAiMart.Infrastructure.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly AppDbContext _context;

    public AdminRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<AdminUser?> GetByUsernameAsync(string username)
        => await _context.AdminUsers.FirstOrDefaultAsync(u => u.Username == username);

    public async Task<AdminUser> CreateAsync(AdminUser admin)
    {
        _context.AdminUsers.Add(admin);
        await _context.SaveChangesAsync();
        return admin;
    }

    public async Task<bool> ExistsAsync()
        => await _context.AdminUsers.AnyAsync();
}
