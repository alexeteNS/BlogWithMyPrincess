namespace BlogWithMyPrincess.Dtos.Comment;

public class CreateComment
{
    public int? PostId { get; set; }    
    public int? CommentId { get; set; }
    
    public int UserId { get; set; }
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
    public string? Image { get; set; }
}