using BlogWithMyPrincess.Entities;

namespace BlogWithMyPrincess.Interfaces;

public interface IPostRepository
{
    public Task<Post> CreatePost(Post? post);
    public Task<Post> UpdatePost(Post post);
    public Task<Post> GetCompletePostById(int postId);
    public Task<Post> GetPostById(int id);
    public Task<List<Post>> GetAllPosts();
    public Task<List<Post>> GetAllPostsByAuthor(int authorId);
    public Task<bool> DeletePost(int id);
}