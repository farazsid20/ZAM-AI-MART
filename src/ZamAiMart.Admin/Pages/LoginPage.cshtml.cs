using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZamAiMart.Core.DTOs;
using ZamAiMart.Core.Interfaces;

namespace ZamAiMart.Admin.Pages;

public class LoginPageModel : PageModel
{
    private readonly IAdminService _adminService;

    public string? ErrorMessage { get; private set; }

    public LoginPageModel(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public IActionResult OnGet()
    {
        if (User.Identity?.IsAuthenticated == true)
            return RedirectToPage("/_Host");
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(string username, string password)
    {
        var result = await _adminService.AuthenticateAsync(new AdminLoginDto
        {
            Username = username,
            Password = password
        });

        if (!result.Success)
        {
            ErrorMessage = result.Message;
            return Page();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, username),
            new(ClaimTypes.Role, "Admin")
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8) }
        );

        return Redirect("/");
    }
}
