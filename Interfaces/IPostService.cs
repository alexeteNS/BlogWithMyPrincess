using BlogWithMyPrincess.Entities;

namespace BlogWithMyPrincess.Interfaces;

public interface IPostService
{
    public Task<Post> CreatePost(Post? post);
    public Task<Post> UpdatePost(Post? post);
    public Task<Post> GetPostById(int id);
    public Task<List<Post>> GetAllPosts();
    public Task<List<Post>> GetAllPostsByAuthor(string authorId);
    public Task<bool> DeletePostById(int id);
}