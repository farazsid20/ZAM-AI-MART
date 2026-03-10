namespace ZamAiMart.Core.DTOs;

public class AIWebsiteDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal PriceINR { get; set; }
    public bool IsFree { get; set; }
    public string WebsiteURL { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? LogoURL { get; set; }
    public string FormattedPrice => IsFree ? "FREE" : $"₹{PriceINR:N0}";
    public DateTime CreatedAt { get; set; }
}

public class CreateAIWebsiteDto
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal PriceINR { get; set; }
    public bool IsFree { get; set; }
    public string WebsiteURL { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? LogoURL { get; set; }
}

public class UpdateAIWebsiteDto
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public decimal PriceINR { get; set; }
    public bool IsFree { get; set; }
    public string WebsiteURL { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? LogoURL { get; set; }
}
