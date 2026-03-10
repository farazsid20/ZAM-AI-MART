using ZamAiMart.Core.DTOs;
using ZamAiMart.Core.Entities;

namespace ZamAiMart.Core.Interfaces;

public interface IAIToolRepository
{
    Task<PagedResult<AITool>> GetAllAsync(AIToolSearchDto searchDto);
    Task<AITool?> GetByIdAsync(int id);
    Task<IEnumerable<AITool>> GetByCategoryAsync(int categoryId);
    Task<IEnumerable<AITool>> GetFeaturedAsync();
    Task<IEnumerable<AITool>> GetLatestAsync(int count = 12);
    Task<AITool> CreateAsync(AITool tool);
    Task<AITool> UpdateAsync(AITool tool);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<int> GetCountByCategoryAsync(int categoryId);
}
