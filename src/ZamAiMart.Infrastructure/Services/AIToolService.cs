using ZamAiMart.Core.DTOs;
using ZamAiMart.Core.Entities;
using ZamAiMart.Core.Enums;
using ZamAiMart.Core.Interfaces;

namespace ZamAiMart.Infrastructure.Services;

public class AIToolService : IAIToolService
{
    private readonly IAIToolRepository _repository;

    public AIToolService(IAIToolRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagedResult<AIToolDto>> GetAllAsync(AIToolSearchDto searchDto)
    {
        var result = await _repository.GetAllAsync(searchDto);
        return new PagedResult<AIToolDto>
        {
            Items = result.Items.Select(MapToDto).ToList(),
            TotalCount = result.TotalCount,
            Page = result.Page,
            PageSize = result.PageSize
        };
    }

    public async Task<AIToolDto?> GetByIdAsync(int id)
    {
        var tool = await _repository.GetByIdAsync(id);
        return tool == null ? null : MapToDto(tool);
    }

    public async Task<IEnumerable<AIToolDto>> GetByCategoryAsync(int categoryId)
    {
        var tools = await _repository.GetByCategoryAsync(categoryId);
        return tools.Select(MapToDto);
    }

    public async Task<IEnumerable<AIToolDto>> GetFeaturedAsync()
    {
        var tools = await _repository.GetFeaturedAsync();
        return tools.Select(MapToDto);
    }

    public async Task<IEnumerable<AIToolDto>> GetLatestAsync(int count = 12)
    {
        var tools = await _repository.GetLatestAsync(count);
        return tools.Select(MapToDto);
    }

    public async Task<AIToolDto> CreateAsync(CreateAIToolDto dto)
    {
        var tool = new AITool
        {
            Name = dto.Name,
            Description = dto.Description,
            LogoUrl = dto.LogoUrl,
            WebsiteUrl = dto.WebsiteUrl,
            CategoryId = dto.CategoryId,
            Price = dto.Price,
            PricingType = dto.PricingType,
            IsFree = dto.IsFree,
            IsFeatured = dto.IsFeatured,
            Tags = dto.Tags
        };
        var created = await _repository.CreateAsync(tool);
        var fullTool = await _repository.GetByIdAsync(created.Id);
        return MapToDto(fullTool!);
    }

    public async Task<AIToolDto?> UpdateAsync(int id, UpdateAIToolDto dto)
    {
        var tool = await _repository.GetByIdAsync(id);
        if (tool == null) return null;

        tool.Name = dto.Name;
        tool.Description = dto.Description;
        tool.LogoUrl = dto.LogoUrl;
        tool.WebsiteUrl = dto.WebsiteUrl;
        tool.CategoryId = dto.CategoryId;
        tool.Price = dto.Price;
        tool.PricingType = dto.PricingType;
        tool.IsFree = dto.IsFree;
        tool.IsFeatured = dto.IsFeatured;
        tool.Tags = dto.Tags;

        await _repository.UpdateAsync(tool);
        var updated = await _repository.GetByIdAsync(id);
        return MapToDto(updated!);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }

    private static string FormatPrice(AITool tool)
    {
        if (tool.IsFree) return "FREE";
        return tool.PricingType switch
        {
            PricingType.Monthly => $"₹{tool.Price:N0}/month",
            PricingType.Yearly => $"₹{tool.Price:N0}/year",
            PricingType.OneTime => $"₹{tool.Price:N0} one-time",
            PricingType.Freemium => tool.Price > 0 ? $"Free + ₹{tool.Price:N0}/month" : "Freemium",
            _ => "FREE"
        };
    }

    private static AIToolDto MapToDto(AITool t) => new()
    {
        Id = t.Id,
        Name = t.Name,
        Description = t.Description,
        LogoUrl = t.LogoUrl,
        WebsiteUrl = t.WebsiteUrl,
        CategoryId = t.CategoryId,
        CategoryName = t.Category?.Name ?? string.Empty,
        CategoryIcon = t.Category?.Icon ?? string.Empty,
        Price = t.Price,
        PricingType = t.PricingType,
        IsFree = t.IsFree,
        IsFeatured = t.IsFeatured,
        Tags = t.Tags,
        FormattedPrice = FormatPrice(t),
        CreatedAt = t.CreatedAt
    };
}
