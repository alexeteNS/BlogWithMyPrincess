namespace BlogWithMyPrincess.Dtos.Comment;

public class BackComment
{
    public int Id { get; set; }
    public string? Text { get; set; } = string.Empty;
    public string? ImageUrl { get; set; } = string.Empty;
    public DateTime DateCreated { get; set; }
    public int? dislikes { get; set; } = 0;
    public int? likes { get; set; } = 0;
    public int UserId  { get; set; }   
    public int? postId { get; set; }
    public int? commentId{ get; set; }
}