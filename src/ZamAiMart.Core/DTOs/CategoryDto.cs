namespace ZamAiMart.Core.DTOs;

public class CategoryDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = string.Empty;
}

public class CreateCategoryDto
{
    public string CategoryName { get; set; } = string.Empty;
}
