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
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var usePostgres = string.Equals(configuration["UsePostgreSQL"], "true", StringComparison.OrdinalIgnoreCase);
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (usePostgres && !string.IsNullOrEmpty(connectionString))
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString));
        }
        else
        {
            var sqliteConn = configuration.GetConnectionString("SQLite") ?? "Data Source=ZamAiMart.db";
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(sqliteConn));
        }

        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IAIToolRepository, AIToolRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAIToolService, AIToolService>();

        return services;
    }
}
