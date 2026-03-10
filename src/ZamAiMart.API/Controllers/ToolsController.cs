using Microsoft.AspNetCore.Mvc;
using ZamAiMart.Core.DTOs;
using ZamAiMart.Core.Interfaces;

namespace ZamAiMart.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToolsController : ControllerBase
{
    private readonly IAIToolService _service;
    private readonly ILogger<ToolsController> _logger;

    public ToolsController(IAIToolService service, ILogger<ToolsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<AIToolDto>>> GetAll([FromQuery] AIToolSearchDto searchDto)
    {
        var result = await _service.GetAllAsync(searchDto);
        return Ok(result);
    }

    [HttpGet("featured")]
    public async Task<ActionResult<IEnumerable<AIToolDto>>> GetFeatured()
    {
        var tools = await _service.GetFeaturedAsync();
        return Ok(tools);
    }

    [HttpGet("latest")]
    public async Task<ActionResult<IEnumerable<AIToolDto>>> GetLatest([FromQuery] int count = 12)
    {
        var tools = await _service.GetLatestAsync(count);
        return Ok(tools);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<AIToolDto>> GetById(int id)
    {
        var tool = await _service.GetByIdAsync(id);
        if (tool == null) return NotFound();
        return Ok(tool);
    }

    [HttpGet("category/{categoryId:int}")]
    public async Task<ActionResult<IEnumerable<AIToolDto>>> GetByCategory(int categoryId)
    {
        var tools = await _service.GetByCategoryAsync(categoryId);
        return Ok(tools);
    }

    [HttpPost]
    public async Task<ActionResult<AIToolDto>> Create([FromBody] CreateAIToolDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<AIToolDto>> Update(int id, [FromBody] UpdateAIToolDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();
        return NoContent();
    }
}
