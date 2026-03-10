using ZamAiMart.Core.DTOs;
using ZamAiMart.Core.Entities;
using ZamAiMart.Core.Interfaces;

namespace ZamAiMart.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repo;

    public CategoryService(ICategoryRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();
        return items.Select(c => new CategoryDto { Id = c.Id, CategoryName = c.CategoryName });
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var entity = new Category { CategoryName = dto.CategoryName };
        var created = await _repo.CreateAsync(entity);
        return new CategoryDto { Id = created.Id, CategoryName = created.CategoryName };
    }
}
