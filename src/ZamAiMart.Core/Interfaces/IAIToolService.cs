using ZamAiMart.Core.DTOs;

namespace ZamAiMart.Core.Interfaces;

public interface IAIToolService
{
    Task<PagedResult<AIToolDto>> GetAllAsync(AIToolSearchDto searchDto);
    Task<AIToolDto?> GetByIdAsync(int id);
    Task<IEnumerable<AIToolDto>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<AIToolDto>> GetFeaturedAsync();
    Task<IEnumerable<AIToolDto>> GetLatestAsync(int count = 12);
    Task<AIToolDto> CreateAsync(CreateAIToolDto dto);
    Task<AIToolDto?> UpdateAsync(int id, UpdateAIToolDto dto);
    Task<bool> DeleteAsync(int id);
}
