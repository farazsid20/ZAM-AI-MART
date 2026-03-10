using Microsoft.EntityFrameworkCore;
using ZamAiMart.Core.DTOs;
using ZamAiMart.Core.Entities;
using ZamAiMart.Core.Interfaces;
using ZamAiMart.Infrastructure.Data;

namespace ZamAiMart.Infrastructure.Repositories;

public class AIToolRepository : IAIToolRepository
{
    private readonly AppDbContext _context;

    public AIToolRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<AITool>> GetAllAsync(AIToolSearchDto searchDto)
    {
        var query = _context.AITools.Include(t => t.Category).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchDto.Query))
        {
            var q = searchDto.Query.ToLower();
            query = query.Where(t =>
                t.Name.ToLower().Contains(q) ||
                t.Description.ToLower().Contains(q) ||
                (t.Tags != null && t.Tags.ToLower().Contains(q)));
        }

        if (searchDto.CategoryId.HasValue)
            query = query.Where(t => t.CategoryId == searchDto.CategoryId.Value);

        if (searchDto.IsFree.HasValue)
            query = query.Where(t => t.IsFree == searchDto.IsFree.Value);

        if (searchDto.IsFeatured.HasValue)
            query = query.Where(t => t.IsFeatured == searchDto.IsFeatured.Value);

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderByDescending(t => t.IsFeatured)
            .ThenByDescending(t => t.CreatedAt)
            .Skip((searchDto.Page - 1) * searchDto.PageSize)
            .Take(searchDto.PageSize)
            .ToListAsync();

        return new PagedResult<AITool>
        {
            Items = items,
            TotalCount = totalCount,
            Page = searchDto.Page,
            PageSize = searchDto.PageSize
        };
    }

    public async Task<AITool?> GetByIdAsync(int id)
    {
        return await _context.AITools
            .Include(t => t.Category)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<AITool>> GetByCategoryAsync(int categoryId)
    {
        return await _context.AITools
            .Include(t => t.Category)
            .Where(t => t.CategoryId == categoryId)
            .OrderByDescending(t => t.IsFeatured)
            .ThenBy(t => t.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<AITool>> GetFeaturedAsync()
    {
        return await _context.AITools
            .Include(t => t.Category)
            .Where(t => t.IsFeatured)
            .OrderByDescending(t => t.CreatedAt)
            .Take(12)
            .ToListAsync();
    }

    public async Task<IEnumerable<AITool>> GetLatestAsync(int count = 12)
    {
        return await _context.AITools
            .Include(t => t.Category)
            .OrderByDescending(t => t.CreatedAt)
            .Take(count)
            .ToListAsync();
    }

    public async Task<AITool> CreateAsync(AITool tool)
    {
        tool.CreatedAt = DateTime.UtcNow;
        tool.UpdatedAt = DateTime.UtcNow;
        _context.AITools.Add(tool);
        await _context.SaveChangesAsync();
        return tool;
    }

    public async Task<AITool> UpdateAsync(AITool tool)
    {
        tool.UpdatedAt = DateTime.UtcNow;
        _context.AITools.Update(tool);
        await _context.SaveChangesAsync();
        return tool;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var tool = await _context.AITools.FindAsync(id);
        if (tool == null) return false;
        _context.AITools.Remove(tool);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.AITools.AnyAsync(t => t.Id == id);
    }

    public async Task<int> GetCountByCategoryAsync(int categoryId)
    {
        return await _context.AITools.CountAsync(t => t.CategoryId == categoryId);
    }
}
