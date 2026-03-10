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

    // Tracks whether the live API is reachable (null = not yet tested)
    private bool? _apiReachable = null;

    public ApiService(HttpClient http)
    {
        _http = http;
    }

    // ── public flag that pages can read ───────────────────────────────────────
    public bool IsOfflineMode => _apiReachable == false;

    // ── Categories ────────────────────────────────────────────────────────────
    public async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        if (_apiReachable == false) return StaticData.GetCategories();
        try
        {
            var result = await _http.GetFromJsonAsync<List<CategoryDto>>("api/categories", _options);
            if (result != null && result.Count > 0) { _apiReachable = true; return result; }
            _apiReachable = false;
            return StaticData.GetCategories();
        }
        catch { _apiReachable = false; return StaticData.GetCategories(); }
    }

    public async Task<CategoryDto?> GetCategoryAsync(int id)
    {
        if (_apiReachable == false) return StaticData.GetCategories().FirstOrDefault(c => c.Id == id);
        try { return await _http.GetFromJsonAsync<CategoryDto>($"api/categories/{id}", _options); }
        catch { return StaticData.GetCategories().FirstOrDefault(c => c.Id == id); }
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

    // ── AI Tools ──────────────────────────────────────────────────────────────
    public async Task<PagedResult<AIToolDto>> GetToolsAsync(AIToolSearchDto searchDto)
    {
        if (_apiReachable == false) return StaticData.SearchTools(searchDto);
        try
        {
            var query  = BuildSearchQuery(searchDto);
            var result = await _http.GetFromJsonAsync<PagedResult<AIToolDto>>($"api/tools?{query}", _options);
            if (result != null && result.TotalCount > 0) { _apiReachable = true; return result; }
            _apiReachable = false;
            return StaticData.SearchTools(searchDto);
        }
        catch { _apiReachable = false; return StaticData.SearchTools(searchDto); }
    }

    public async Task<List<AIToolDto>> GetFeaturedToolsAsync()
    {
        if (_apiReachable == false) return StaticData.GetFeaturedTools();
        try
        {
            var result = await _http.GetFromJsonAsync<List<AIToolDto>>("api/tools/featured", _options);
            if (result != null && result.Count > 0) { _apiReachable = true; return result; }
            _apiReachable = false;
            return StaticData.GetFeaturedTools();
        }
        catch { _apiReachable = false; return StaticData.GetFeaturedTools(); }
    }

    public async Task<List<AIToolDto>> GetLatestToolsAsync(int count = 12)
    {
        if (_apiReachable == false) return StaticData.GetLatestTools(count);
        try
        {
            var result = await _http.GetFromJsonAsync<List<AIToolDto>>($"api/tools/latest?count={count}", _options);
            if (result != null && result.Count > 0) { _apiReachable = true; return result; }
            _apiReachable = false;
            return StaticData.GetLatestTools(count);
        }
        catch { _apiReachable = false; return StaticData.GetLatestTools(count); }
    }

    public async Task<AIToolDto?> GetToolAsync(int id)
    {
        if (_apiReachable == false) return StaticData.GetTool(id);
        try { return await _http.GetFromJsonAsync<AIToolDto>($"api/tools/{id}", _options); }
        catch { return StaticData.GetTool(id); }
    }

    public async Task<List<AIToolDto>> GetToolsByCategoryAsync(int categoryId)
    {
        if (_apiReachable == false) return StaticData.GetToolsByCategory(categoryId);
        try
        {
            var result = await _http.GetFromJsonAsync<List<AIToolDto>>($"api/tools/category/{categoryId}", _options);
            if (result != null) { _apiReachable = true; return result; }
            _apiReachable = false;
            return StaticData.GetToolsByCategory(categoryId);
        }
        catch { _apiReachable = false; return StaticData.GetToolsByCategory(categoryId); }
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
        if (dto.IsFree.HasValue)     parts.Add($"isFree={dto.IsFree}");
        if (dto.IsFeatured.HasValue) parts.Add($"isFeatured={dto.IsFeatured}");
        parts.Add($"page={dto.Page}");
        parts.Add($"pageSize={dto.PageSize}");
        return string.Join("&", parts);
    }
}
