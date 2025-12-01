using BlogWithMyPrincess.Entities;
using BlogWithMyPrincess.Interfaces;

namespace BlogWithMyPrincess.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Post> CreatePost(string? content, string? imageUrl, int userId)
    {
        if(string.IsNullOrWhiteSpace(content) && string.IsNullOrEmpty(imageUrl)) return null;
        Post post = new Post
        {
            Text= content,
            ImageUrl = imageUrl,
            userId= userId
        };
        
        return await _postRepository.CreatePost(post);
    }

    public async Task<Post> UpdatePost(string? content, string? imageUrl, int postId)
    {
        var post = await GetPostById(postId);
        if (post == null) return null;
        
        if (!string.IsNullOrWhiteSpace(content))
            post.Text = content;

        if (!string.IsNullOrWhiteSpace(imageUrl))
            post.ImageUrl = imageUrl;
        
        if (content == null && imageUrl == null)
            return null;

        return await _postRepository.UpdatePost(post);
    }

    public async Task<Post> GetPostById(int postId)
    {
        return await _postRepository.GetPostById(postId);
    }

    public async Task<List<Post>> GetAllPosts()
    {
        return await _postRepository.GetAllPosts();
    }

    public async Task<List<Post>> GetAllPostsByAuthor(int authorId)
    {
        return await _postRepository.GetAllPostsByAuthor(authorId);
    }

    public async Task<bool> DeletePostById(int id)
    {
        return await _postRepository.DeletePost(id);
    }
}