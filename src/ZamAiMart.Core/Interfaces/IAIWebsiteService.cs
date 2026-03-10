using ZamAiMart.Core.DTOs;

namespace ZamAiMart.Core.Interfaces;

public interface IAIWebsiteService
{
    Task<IEnumerable<AIWebsiteDto>> GetAllAsync();
    Task<IEnumerable<AIWebsiteDto>> GetByCategoryAsync(string category);
    Task<AIWebsiteDto?> GetByIdAsync(int id);
    Task<AIWebsiteDto> CreateAsync(CreateAIWebsiteDto dto);
    Task<AIWebsiteDto?> UpdateAsync(int id, UpdateAIWebsiteDto dto);
    Task<bool> DeleteAsync(int id);
}
