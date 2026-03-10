using Microsoft.EntityFrameworkCore;
using ZamAiMart.Core.Entities;

namespace ZamAiMart.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<AIWebsite> AIWebsites => Set<AIWebsite>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AIWebsite>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            entity.Property(e => e.PriceINR).HasPrecision(18, 2);
            entity.Property(e => e.WebsiteURL).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.LogoURL).HasMaxLength(500);
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.CategoryName).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<AdminUser>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(100);
            entity.HasIndex(e => e.Username).IsUnique();
            entity.Property(e => e.PasswordHash).IsRequired();
        });
    }
}
