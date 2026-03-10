using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using ZamAiMart.Client.Models;

namespace ZamAiMart.Client.Services;

public class ApiService
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _options = new()
    {
        PropertyNameCaseInsensitive = true,
        Converters = { new JsonStringEnumConverter() }
    };

    public ApiService(HttpClient http)
    {
        _http = http;
    }

    // Categories
    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        try
        {
            return await _http.GetFromJsonAsync<List<CategoryDto>>("api/categories", _options) ?? new();
        }
        catch { return new(); }
    }

    public async Task<CategoryDto?> GetCategoryAsync(int id)
    {
        try { return await _http.GetFromJsonAsync<CategoryDto>($"api/categories/{id}", _options); }
        catch { return null; }
    }

    public async Task<CategoryDto?> CreateCategoryAsync(CreateCategoryDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/categories", dto, _options);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<CategoryDto>(_options);
        return null;
    }

    public async Task<CategoryDto?> UpdateCategoryAsync(int id, UpdateCategoryDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/categories/{id}", dto, _options);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<CategoryDto>(_options);
        return null;
    }

    public async Task<bool> DeleteCategoryAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/categories/{id}");
        return response.IsSuccessStatusCode;
    }

    // AI Tools
    public async Task<PagedResult<AIToolDto>> GetToolsAsync(AIToolSearchDto searchDto)
    {
        try
        {
            var query = BuildSearchQuery(searchDto);
            return await _http.GetFromJsonAsync<PagedResult<AIToolDto>>($"api/tools?{query}", _options)
                   ?? new PagedResult<AIToolDto>();
        }
        catch { return new PagedResult<AIToolDto>(); }
    }

    public async Task<List<AIToolDto>> GetFeaturedToolsAsync()
    {
        try { return await _http.GetFromJsonAsync<List<AIToolDto>>("api/tools/featured", _options) ?? new(); }
        catch { return new(); }
    }

    public async Task<List<AIToolDto>> GetLatestToolsAsync(int count = 12)
    {
        try { return await _http.GetFromJsonAsync<List<AIToolDto>>($"api/tools/latest?count={count}", _options) ?? new(); }
        catch { return new(); }
    }

    public async Task<AIToolDto?> GetToolAsync(int id)
    {
        try { return await _http.GetFromJsonAsync<AIToolDto>($"api/tools/{id}", _options); }
        catch { return null; }
    }

    public async Task<List<AIToolDto>> GetToolsByCategoryAsync(int categoryId)
    {
        try { return await _http.GetFromJsonAsync<List<AIToolDto>>($"api/tools/category/{categoryId}", _options) ?? new(); }
        catch { return new(); }
    }

    public async Task<AIToolDto?> CreateToolAsync(CreateAIToolDto dto)
    {
        var response = await _http.PostAsJsonAsync("api/tools", dto, _options);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<AIToolDto>(_options);
        return null;
    }

    public async Task<AIToolDto?> UpdateToolAsync(int id, UpdateAIToolDto dto)
    {
        var response = await _http.PutAsJsonAsync($"api/tools/{id}", dto, _options);
        if (response.IsSuccessStatusCode)
            return await response.Content.ReadFromJsonAsync<AIToolDto>(_options);
        return null;
    }

    public async Task<bool> DeleteToolAsync(int id)
    {
        var response = await _http.DeleteAsync($"api/tools/{id}");
        return response.IsSuccessStatusCode;
    }

    private static string BuildSearchQuery(AIToolSearchDto dto)
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(dto.Query)) parts.Add($"query={Uri.EscapeDataString(dto.Query)}");
        if (dto.CategoryId.HasValue) parts.Add($"categoryId={dto.CategoryId}");
        if (dto.IsFree.HasValue) parts.Add($"isFree={dto.IsFree}");
        if (dto.IsFeatured.HasValue) parts.Add($"isFeatured={dto.IsFeatured}");
        parts.Add($"page={dto.Page}");
        parts.Add($"pageSize={dto.PageSize}");
        return string.Join("&", parts);
    }
}
