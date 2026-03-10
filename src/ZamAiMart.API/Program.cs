using ZamAiMart.Infrastructure;
using ZamAiMart.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();
builder.Services.AddHealthChecks();

// CORS - allow GitHub Pages + local dev + Docker (any origin for production if needed)
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>()
    ?? new[]
    {
        "http://localhost:5001",
        "https://localhost:5001",
        "http://localhost:80",
        "https://farazsid20.github.io"   // GitHub Pages URL
    };

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient", policy =>
    {
        policy.WithOrigins(allowedOrigins)
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

    // Open policy for Docker/nginx scenario (same origin via proxy)
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Auto-migrate and seed on startup
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        context.Database.EnsureCreated();
        await SeedData.SeedAsync(context);
    }
    catch (Exception ex)
    {
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Database seeding failed: {Message}", ex.Message);
    }
}

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// In production behind Docker/nginx, skip HTTPS redirect (nginx handles it)
if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

// Use open CORS in Docker/nginx (reverse proxy), or restricted for direct access
var corsPolicy = app.Environment.IsProduction() ? "AllowAll" : "AllowBlazorClient";
app.UseCors(corsPolicy);

app.UseResponseCaching();
app.UseAuthorization();

// Health check endpoint (used by Docker HEALTHCHECK + load balancers)
app.MapHealthChecks("/health");

app.MapControllers();

app.Run();
