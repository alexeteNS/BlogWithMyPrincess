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
    
    public async Task<Comments?> CreateComment(Comments? comment)
    {
        if (comment == null) return null;
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return comment;
    }

    public async Task<bool> DeleteComment(int commentId)
    {
        var comment = await _context.Comments
            .Include(c => c.Replies)
            .FirstOrDefaultAsync(c => c.Id == commentId);

        if (comment == null) return false;
        
        await DeleteChildren(comment.Id);

       _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();

        return true;
    }
        
    private async Task DeleteChildren(int commentId)
    {
        var children = await _context.Comments
            .Where(c => c.IdCommentParent == commentId)
            .ToListAsync();

        foreach (var child in children)
        {
            await DeleteChildren(child.Id);
            _context.Comments.Remove(child);
        }
    }
    
    public async Task<Comments?> EditComment(int commentId, string newText)
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
        return await _context.Comments
            .Where(c => c.IdPost == id && c.IdCommentParent == null) // SOLO comentarios raíz del post
            .ToListAsync();
    }
}