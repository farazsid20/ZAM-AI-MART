using ZamAiMart.Core.DTOs;
using ZamAiMart.Core.Entities;
using ZamAiMart.Core.Interfaces;

namespace ZamAiMart.Infrastructure.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(MapToDto);
    }

    public async Task<CategoryDto?> GetByIdAsync(int id)
    {
        var category = await _repository.GetByIdAsync(id);
        return category == null ? null : MapToDto(category);
    }

    public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Name = dto.Name,
            Slug = dto.Slug,
            Icon = dto.Icon,
            Description = dto.Description
        };
        var created = await _repository.CreateAsync(category);
        return MapToDto(created);
    }

    public async Task<CategoryDto?> UpdateAsync(int id, UpdateCategoryDto dto)
    {
        var category = await _repository.GetByIdAsync(id);
        if (category == null) return null;

        category.Name = dto.Name;
        category.Slug = dto.Slug;
        category.Icon = dto.Icon;
        category.Description = dto.Description;

        var updated = await _repository.UpdateAsync(category);
        return MapToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static CategoryDto MapToDto(Category c) => new()
    {
        Id = c.Id,
        Name = c.Name,
        Slug = c.Slug,
        Icon = c.Icon,
        Description = c.Description,
        ToolCount = c.AITools?.Count ?? 0,
        CreatedAt = c.CreatedAt
    };
}
