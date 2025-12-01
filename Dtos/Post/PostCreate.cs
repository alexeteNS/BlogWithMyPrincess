namespace BlogWithMyPrincess.Dtos.Post;

public class PostCreate
{
    public string? Content { get; set; }
    public string? ImageUrl { get; set; }
    public int UserId { get; set; }
}