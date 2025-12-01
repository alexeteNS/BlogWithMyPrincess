using BlogWithMyPrincess.Dtos.Post;
using BlogWithMyPrincess.Entities;

namespace BlogWithMyPrincess.Interfaces;

public interface IPostService
{
    public Task<BackPostInfo> CreatePost(string? content, string? imageUrl, int userId);
    public Task<BackPostInfo> UpdatePost(string? content, string? ImageUrl, int postId);
    public Task<Post> GetCompletePostById(int postId);
    public Task<BackPostInfo> GetPostById(int id);
    public Task<List<BackPostInfo>> GetAllPosts();
    public Task<List<BackPostInfo>> GetAllPostsByAuthor(int authorId);
    public Task<bool> DeletePostById(int id);
}