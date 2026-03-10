using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ZamAiMart.Core.Interfaces;
using ZamAiMart.Infrastructure.Data;
using ZamAiMart.Infrastructure.Repositories;
using ZamAiMart.Infrastructure.Services;

namespace ZamAiMart.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("SqlServerConnection")
            ?? throw new InvalidOperationException("Connection string 'SqlServerConnection' not found.");

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IAIWebsiteRepository, AIWebsiteRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAdminRepository, AdminRepository>();

        services.AddScoped<IAIWebsiteService, AIWebsiteService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAdminService, AdminService>();

        return services;
    }
}
