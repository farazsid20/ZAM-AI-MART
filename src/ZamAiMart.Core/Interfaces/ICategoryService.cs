using ZamAiMart.Core.DTOs;

namespace ZamAiMart.Core.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto> CreateAsync(CreateCategoryDto dto);
}
