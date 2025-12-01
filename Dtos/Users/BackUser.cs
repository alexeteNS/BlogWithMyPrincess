namespace BlogWithMyPrincess.Dtos.Users;

public class BackUser
{
    public string username { get; set; }
    public string password { get; set; }
    public string email { get; set; }

    public List<BackComments> comments { get; set; } = new();
    public List<BackPost> posts { get; set; } = new();
}

public class BackComments
{
    public int Id { get; set; }
    public int? postId { get; set; }
    public int? commentId{ get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public DateTime Time { get; set; }
}

public class BackPost
{
    public int Id { get; set; }
    public string Content { get; set; }
    public string ImageUrl { get; set; }
    public DateTime Time { get; set; }
}