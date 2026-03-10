namespace ZamAiMart.Core.Entities;

public class AIWebsite
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal PriceINR { get; set; }
    public bool IsFree { get; set; }
    public string WebsiteURL { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? LogoURL { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
