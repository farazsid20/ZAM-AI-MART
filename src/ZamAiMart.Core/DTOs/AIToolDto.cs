using ZamAiMart.Core.Enums;

namespace ZamAiMart.Core.DTOs;

public class AIToolDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string WebsiteUrl { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = string.Empty;
    public string CategoryIcon { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public PricingType PricingType { get; set; }
    public bool IsFree { get; set; }
    public bool IsFeatured { get; set; }
    public string? Tags { get; set; }
    public string FormattedPrice { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}

public class CreateAIToolDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string WebsiteUrl { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public PricingType PricingType { get; set; } = PricingType.Free;
    public bool IsFree { get; set; } = true;
    public bool IsFeatured { get; set; } = false;
    public string? Tags { get; set; }
}

public class UpdateAIToolDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string WebsiteUrl { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public decimal Price { get; set; }
    public PricingType PricingType { get; set; }
    public bool IsFree { get; set; }
    public bool IsFeatured { get; set; }
    public string? Tags { get; set; }
}

public class AIToolSearchDto
{
    public string? Query { get; set; }
    public int? CategoryId { get; set; }
    public bool? IsFree { get; set; }
    public bool? IsFeatured { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 20;
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = new();
    public int TotalCount { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}
