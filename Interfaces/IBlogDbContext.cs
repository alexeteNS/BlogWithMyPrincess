namespace BlogWithMyPrincess.Interfaces;
using  Microsoft.EntityFrameworkCore;
using BlogWithMyPrincess.Entities;
public interface IBlogDbContext
{
       public DbSet<User> Users { get; set; }
       public DbSet<Comments> Comments { get; set; }
       public DbSet<Post> Posts { get; set; }
       
       Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
       
}