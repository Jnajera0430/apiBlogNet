using Microsoft.EntityFrameworkCore;
using OpenApi.Models;

public class AppContext : DbContext
{
  public AppContext(DbContextOptions<AppContext> options) : base(options)
  {
  }

  public DbSet<Blog> blogs { get; set; }
  public DbSet<User> users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Blog>(blog =>
    {
      blog.ToTable("blogs");
      blog.HasKey(b => b.id);
      blog.HasOne(b => b.user)
              .WithMany(u => u.blogs)
              .HasForeignKey("userId");
      blog.Property(b => b.title).IsRequired().HasMaxLength(100);
      blog.Property(b => b.auhtor).IsRequired().HasMaxLength(100);
      blog.Property(b => b.url).IsRequired(false);
      blog.Property(b => b.likes).HasDefaultValue<int>(0);
    });

    modelBuilder.Entity<User>(user =>
        {
          user.ToTable("users");
          user.HasKey(u => u.id);

          user.HasMany(u => u.blogs)
              .WithOne(b => b.user);

          user.Property(u => u.username).IsRequired().HasMaxLength(100);
          user.Property(u => u.name).IsRequired().HasMaxLength(100);
          user.Property(u => u.password).IsRequired().HasMaxLength(255);
        });
  }
}
