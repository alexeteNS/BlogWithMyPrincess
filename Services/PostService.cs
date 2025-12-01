using BlogWithMyPrincess.Dtos.Post;
using BlogWithMyPrincess.Dtos.Users;
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

    public async Task<BackPostInfo> CreatePost(string? content, string? imageUrl, int userId)
    {
        if(string.IsNullOrWhiteSpace(content) && string.IsNullOrEmpty(imageUrl)) return null;
        Post newPost = new Post
        {
            Text= content,
            ImageUrl = imageUrl,
            userId= userId
        };
        
        var post = await _postRepository.CreatePost(newPost);
        return new BackPostInfo
        {
            Id = post.Id,
            Text = post.Text,
            ImageUrl = post.ImageUrl,
            DateCreated = post.DateCreated,
            dislikes = post.dislikes,
            likes = post.likes,
            UserId = post.userId
        };
    }

    public async Task<BackPostInfo> UpdatePost(string? content, string? imageUrl, int postId)
    {
        var updatePost = await GetCompletePostById(postId);
        if (updatePost == null) return null;
        
        if (!string.IsNullOrWhiteSpace(content))
            updatePost.Text = content;

        if (!string.IsNullOrWhiteSpace(imageUrl))
            updatePost.ImageUrl = imageUrl;
        
        if (content == null && imageUrl == null)
            return null;

        var post =  await _postRepository.UpdatePost(updatePost);
        return new BackPostInfo
        {
            Id = post.Id,
            Text = post.Text,
            ImageUrl = post.ImageUrl,
            DateCreated = post.DateCreated,
            dislikes = post.dislikes,
            likes = post.likes,
            UserId = post.userId
        };
    }

    public async Task<Post> GetCompletePostById(int postId)
    {
        return await _postRepository.GetCompletePostById(postId);
    }

    public async Task<BackPostInfo> GetPostById(int postId)
    {
        var post = await _postRepository.GetPostById(postId);
        return new BackPostInfo
        {
            Id = post.Id,
            Text = post.Text,
            ImageUrl = post.ImageUrl,
            DateCreated = post.DateCreated,
            dislikes = post.dislikes,
            likes = post.likes,
            UserId = post.userId
        };
    }

    public async Task<List<BackPostInfo>> GetAllPosts()
    {
        var comments =  await _postRepository.GetAllPosts();
        var commentsInfo = new List<BackPostInfo>();
        foreach (var item in comments)
        {
            commentsInfo.Add(new BackPostInfo
            {
                Id = item.Id,
                Text = item.Text,
                ImageUrl = item.ImageUrl,
                DateCreated = item.DateCreated,
                dislikes = item.dislikes,
                likes = item.likes,
                UserId = item.userId
            });
        }

        return commentsInfo;
    }

    public async Task<List<BackPostInfo>> GetAllPostsByAuthor(int authorId)
    {
        var comments= await _postRepository.GetAllPostsByAuthor(authorId);
        var commentsInfo = new List<BackPostInfo>();
        foreach (var item in comments)
        {
            commentsInfo.Add(new BackPostInfo
            {
                Id = item.Id,
                Text = item.Text,
                ImageUrl = item.ImageUrl,
                DateCreated = item.DateCreated,
                dislikes = item.dislikes,
                likes = item.likes,
                UserId = item.userId
            });
        }

        return commentsInfo;
    }

    public async Task<bool> DeletePostById(int id)
    {
        return await _postRepository.DeletePost(id);
    }
}