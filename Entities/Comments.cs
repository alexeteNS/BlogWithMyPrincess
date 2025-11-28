using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BlogWithMyPrincess.Entities;

public class Comments
{
    public int Id { get; set; }
    public string? Text { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    public Reaccion Reaccion { get; set; }
    
    public int? IdCommentParent { get; set; }
    [ForeignKey(nameof(IdCommentParent))]
    public Comments? CommentParent { get; set; }
    public List<Comments?> Replies { get; set; } = new();
    
    public int? IdPost { get; set; }
    [ForeignKey(nameof(IdPost))]
    public Post? Post { get; set; }
    
    public int UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}
public enum Reaccion
{
    MeGusta = 0,
    NoMeGusta = 0
}