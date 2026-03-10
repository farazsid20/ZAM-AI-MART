using Microsoft.EntityFrameworkCore;
using ZamAiMart.Core.Entities;
using ZamAiMart.Core.Interfaces;
using ZamAiMart.Infrastructure.Data;

namespace ZamAiMart.Infrastructure.Repositories;

public class AIWebsiteRepository : IAIWebsiteRepository
{
    private readonly AppDbContext _context;

    public AIWebsiteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AIWebsite>> GetAllAsync()
        => await _context.AIWebsites.OrderByDescending(x => x.CreatedAt).ToListAsync();

    public async Task<IEnumerable<AIWebsite>> GetByCategoryAsync(string category)
        => await _context.AIWebsites
            .Where(x => x.Category.ToLower() == category.ToLower())
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();

    public async Task<AIWebsite?> GetByIdAsync(int id)
        => await _context.AIWebsites.FindAsync(id);

    public async Task<AIWebsite> CreateAsync(AIWebsite website)
    {
        website.CreatedAt = DateTime.UtcNow;
        _context.AIWebsites.Add(website);
        await _context.SaveChangesAsync();
        return website;
    }

    public async Task<AIWebsite?> UpdateAsync(int id, AIWebsite website)
    {
        var existing = await _context.AIWebsites.FindAsync(id);
        if (existing is null) return null;

        existing.Name = website.Name;
        existing.Category = website.Category;
        existing.PriceINR = website.PriceINR;
        existing.IsFree = website.IsFree;
        existing.WebsiteURL = website.WebsiteURL;
        existing.Description = website.Description;
        existing.LogoURL = website.LogoURL;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var website = await _context.AIWebsites.FindAsync(id);
        if (website is null) return false;

        _context.AIWebsites.Remove(website);
        await _context.SaveChangesAsync();
        return true;
    }
}
