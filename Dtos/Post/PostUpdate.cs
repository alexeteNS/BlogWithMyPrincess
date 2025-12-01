namespace BlogWithMyPrincess.Dtos.Post;

public class PostUpdate
{
    public int PostId { get; set; }
    public string? Content { get; set; }
    public string? ImageUrl { get; set; }
}