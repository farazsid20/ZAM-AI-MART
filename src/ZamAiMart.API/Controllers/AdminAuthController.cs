using Microsoft.AspNetCore.Mvc;
using ZamAiMart.Core.DTOs;
using ZamAiMart.Core.Interfaces;

namespace ZamAiMart.API.Controllers;

[ApiController]
[Route("api/auth")]
[Produces("application/json")]
public class AdminAuthController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminAuthController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    /// <summary>Authenticate admin and receive a JWT token.</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AdminLoginDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var result = await _adminService.AuthenticateAsync(dto);
        return result.Success ? Ok(result) : Unauthorized(result);
    }
}
