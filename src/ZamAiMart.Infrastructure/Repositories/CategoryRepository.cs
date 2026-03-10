using Microsoft.EntityFrameworkCore;
using ZamAiMart.Core.Entities;
using ZamAiMart.Core.Interfaces;
using ZamAiMart.Infrastructure.Data;

namespace ZamAiMart.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
        => await _context.Categories.OrderBy(x => x.CategoryName).ToListAsync();

    public async Task<Category?> GetByIdAsync(int id)
        => await _context.Categories.FindAsync(id);

    public async Task<Category> CreateAsync(Category category)
    {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return category;
    }
}
