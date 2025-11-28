namespace BlogWithMyPrincess.Entities;

public class Post
{
    public int Id { get; set; }
    public List<Comments> Comments { get; set; } = new();
}