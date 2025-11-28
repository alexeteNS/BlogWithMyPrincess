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
    public DbSet<User> Users { get; set; }
    public DbSet<Comments> Comments { get; set; }
    public DbSet<Post> Posts { get; set; }
}