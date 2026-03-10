using ZamAiMart.Core.Entities;

namespace ZamAiMart.Core.Interfaces;

public interface IAIWebsiteRepository
{
    Task<IEnumerable<AIWebsite>> GetAllAsync();
    Task<IEnumerable<AIWebsite>> GetByCategoryAsync(string category);
    Task<AIWebsite?> GetByIdAsync(int id);
    Task<AIWebsite> CreateAsync(AIWebsite website);
    Task<AIWebsite?> UpdateAsync(int id, AIWebsite website);
    Task<bool> DeleteAsync(int id);
}
