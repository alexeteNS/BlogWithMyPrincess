using BlogWithMyPrincess.Entities;
using BlogWithMyPrincess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogWithMyPrincess.Data.Repositories;

public class CommentsRepository : ICommentsRepository
{
    private readonly IBlogDbContext _context;
    public CommentsRepository(IBlogDbContext context)
    {
        _context = context;
    }
    
    public async Task<bool> CreateComment(Comments? comment)
    {
        if (comment == null) return false;
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteComment(int commentId)
    {
        var comment = await _context.Comments.FindAsync(commentId);
        if (comment == null) return false;

        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Comments?> UpdateComment(int commentId, string newText)
    {
        var comment = await _context.Comments.FindAsync(commentId);
        if (comment == null) return null;
        comment.Text = newText;
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<List<Comments>> GetCommentsByCommentId(int id)
    {
        return await _context.Comments
            .Where(c => c.IdCommentParent == id)
            .ToListAsync();
    }

    public async Task<List<Comments>> GetCommentsByPostId(int id)
    {
        Post? post = await _context.Posts
            .Include(a => a.Comments)
            .FirstOrDefaultAsync(a => a.Id == id);
        if (post == null) return new List<Comments>();

        return post.Comments;
    }
}