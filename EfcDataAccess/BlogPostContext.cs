using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace EfcDataAccess;

public class BlogPostContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<BlogPost> BlogPosts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = ../EfcDataAccess/BlogPost.db");
        optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(blogPost => blogPost.Id);

            // Configure CreatedAt to have a default value of current datetime
            entity.Property(blogPost => blogPost.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });
        modelBuilder.Entity<User>().HasKey(user => user.Id);
    }
}