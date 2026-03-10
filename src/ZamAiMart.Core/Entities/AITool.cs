using ZamAiMart.Core.Enums;

namespace ZamAiMart.Core.Entities;

public class AITool
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public string WebsiteUrl { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    public decimal Price { get; set; }
    public PricingType PricingType { get; set; } = PricingType.Free;
    public bool IsFree { get; set; } = true;
    public bool IsFeatured { get; set; } = false;
    public string? Tags { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
