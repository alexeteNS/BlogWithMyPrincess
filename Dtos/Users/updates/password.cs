namespace BlogWithMyPrincess.Dtos.Users.updates;

public class password
{
    public int userId { get; set; }
    public string oldPassword { get; set; }
    public string newPassword { get; set; }
}