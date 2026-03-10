namespace ZamAiMart.Core.DTOs;

public class AdminLoginDto
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

public class AdminLoginResponseDto
{
    public bool Success { get; set; }
    public string? Token { get; set; }
    public string? Message { get; set; }
}
