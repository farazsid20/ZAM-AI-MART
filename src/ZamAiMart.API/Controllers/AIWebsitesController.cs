using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZamAiMart.Core.DTOs;
using ZamAiMart.Core.Interfaces;

namespace ZamAiMart.API.Controllers;

[ApiController]
[Route("api/aiwebsites")]
[Produces("application/json")]
public class AIWebsitesController : ControllerBase
{
    private readonly IAIWebsiteService _service;

    public AIWebsitesController(IAIWebsiteService service)
    {
        _service = service;
    }

    /// <summary>Get all AI websites, optionally filtered by category.</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] string? category)
    {
        var items = string.IsNullOrWhiteSpace(category)
            ? await _service.GetAllAsync()
            : await _service.GetByCategoryAsync(category);
        return Ok(items);
    }

    /// <summary>Get a single AI website by ID.</summary>
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _service.GetByIdAsync(id);
        return item is null ? NotFound(new { message = $"AI website with ID {id} not found." }) : Ok(item);
    }

    /// <summary>Create a new AI website entry (Admin only).</summary>
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateAIWebsiteDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>Update an existing AI website (Admin only).</summary>
    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateAIWebsiteDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var updated = await _service.UpdateAsync(id, dto);
        return updated is null ? NotFound(new { message = $"AI website with ID {id} not found." }) : Ok(updated);
    }

    /// <summary>Delete an AI website (Admin only).</summary>
    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound(new { message = $"AI website with ID {id} not found." });
    }
}
