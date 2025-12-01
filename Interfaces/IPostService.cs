using BlogWithMyPrincess.Entities;

namespace BlogWithMyPrincess.Interfaces;

public interface IPostService
{
    public Task<Post> CreatePost(string? content, string? imageUrl, int userId);
    public Task<Post> UpdatePost(string? content, string? ImageUrl, int postId);
    public Task<Post> GetPostById(int id);
    public Task<List<Post>> GetAllPosts();
    public Task<List<Post>> GetAllPostsByAuthor(int authorId);
    public Task<bool> DeletePostById(int id);
}