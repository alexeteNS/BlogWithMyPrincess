using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWithMyPrincess.Entities;

public class Post
{
    public int Id { get; set; }
    public string? Text { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public Reaccion Reaccion { get; set; }
    public List<Comments> Comments { get; set; } = new();
    public int userId { get; set; }
    [ForeignKey(nameof(userId))]
    public User? UserParent { get; set; }
    
}