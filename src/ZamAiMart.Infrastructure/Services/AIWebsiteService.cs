using ZamAiMart.Core.DTOs;
using ZamAiMart.Core.Entities;
using ZamAiMart.Core.Interfaces;

namespace ZamAiMart.Infrastructure.Services;

public class AIWebsiteService : IAIWebsiteService
{
    private readonly IAIWebsiteRepository _repo;

    public AIWebsiteService(IAIWebsiteRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<AIWebsiteDto>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();
        return items.Select(ToDto);
    }

    public async Task<IEnumerable<AIWebsiteDto>> GetByCategoryAsync(string category)
    {
        var items = await _repo.GetByCategoryAsync(category);
        return items.Select(ToDto);
    }

    public async Task<AIWebsiteDto?> GetByIdAsync(int id)
    {
        var item = await _repo.GetByIdAsync(id);
        return item is null ? null : ToDto(item);
    }

    public async Task<AIWebsiteDto> CreateAsync(CreateAIWebsiteDto dto)
    {
        var entity = new AIWebsite
        {
            Name = dto.Name,
            Category = dto.Category,
            PriceINR = dto.PriceINR,
            IsFree = dto.IsFree,
            WebsiteURL = dto.WebsiteURL,
            Description = dto.Description,
            LogoURL = dto.LogoURL
        };
        var created = await _repo.CreateAsync(entity);
        return ToDto(created);
    }

    public async Task<AIWebsiteDto?> UpdateAsync(int id, UpdateAIWebsiteDto dto)
    {
        var entity = new AIWebsite
        {
            Name = dto.Name,
            Category = dto.Category,
            PriceINR = dto.PriceINR,
            IsFree = dto.IsFree,
            WebsiteURL = dto.WebsiteURL,
            Description = dto.Description,
            LogoURL = dto.LogoURL
        };
        var updated = await _repo.UpdateAsync(id, entity);
        return updated is null ? null : ToDto(updated);
    }

    public async Task<bool> DeleteAsync(int id)
        => await _repo.DeleteAsync(id);

    private static AIWebsiteDto ToDto(AIWebsite e) => new()
    {
        Id = e.Id,
        Name = e.Name,
        Category = e.Category,
        PriceINR = e.PriceINR,
        IsFree = e.IsFree,
        WebsiteURL = e.WebsiteURL,
        Description = e.Description,
        LogoURL = e.LogoURL,
        CreatedAt = e.CreatedAt
    };
}
