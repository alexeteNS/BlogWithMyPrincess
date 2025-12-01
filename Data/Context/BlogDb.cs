using BlogWithMyPrincess.Entities;
using BlogWithMyPrincess.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace BlogWithMyPrincess.Data.Context;
public class BlogDbContext : DbContext, IBlogDbContext
{
    public BlogDbContext(DbContextOptions<BlogDbContext> options ) : base (options)
    {
        // constructor para inyecciones
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Post>()
            .HasMany(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.IdPost)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Comments>()
            .HasMany(c => c.Replies)
            .WithOne(r => r.CommentParent)
            .HasForeignKey(r => r.IdCommentParent)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Comments> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
}