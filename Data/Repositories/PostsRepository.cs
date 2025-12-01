using BlogWithMyPrincess.Entities;
using BlogWithMyPrincess.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace BlogWithMyPrincess.Data.Repositories;

public class PostsRepository : IPostRepository
{
    private readonly IBlogDbContext _context;

    public PostsRepository(IBlogDbContext context)
    {
        _context = context;
    }
    
    public async Task<Post> CreatePost(Post? post)
    {
        if (post == null) return null;
        await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<Post> UpdatePost(Post post)
    {
        _context.Posts.Update(post);
        await _context.SaveChangesAsync();
        return post;
    }
    public async Task<Post> GetCompletePostById(int postId)
    {
        var post = await _context.Posts.FindAsync(postId);
        if (post == null) return null;
        return post;
    }

    public async Task<Post> GetPostById(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null) return null;
        return post;
    }

    public async Task<List<Post>> GetAllPosts()
    {
        return await _context.Posts.ToListAsync();
    }

    public async Task<List<Post>> GetAllPostsByAuthor(int authorId)
    {
        return await _context.Posts
            .Where(p => p.userId == authorId)
            .ToListAsync();
    }
    
    public async Task<bool> DeletePost(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null) return false;
        
        var rootComments = await _context.Comments
            .Where(c => c.IdPost == id && c.IdCommentParent == null)
            .ToListAsync();
        
        foreach (var comment in rootComments)
        {
            await DeleteCommentTree(comment.Id);
        }
        
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();

        return true;
    }
    
    private async Task DeleteCommentTree(int commentId)
    {
        var children = await _context.Comments
            .Where(c => c.IdCommentParent == commentId)
            .ToListAsync();

        foreach (var child in children)
        {
            await DeleteCommentTree(child.Id);
            _context.Comments.Remove(child);
        }
    }
}