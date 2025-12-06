using BlogWithMyPrincess.Dtos.Comment;
using BlogWithMyPrincess.Dtos.Users;
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
    
    public async Task<BackComment?> CreateComment(Comments? comment)
    {
        if (comment == null) return null;
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        return new BackComment
        {
            Id = comment.Id,
            Text =  comment.Text,
            ImageUrl =  comment.ImageUrl,
            DateCreated =  comment.DateCreated,
            dislikes = comment.dislikes,
            likes = comment.likes,
            UserId = comment.UserId,
            postId = comment.IdPost,
            commentId = comment.IdCommentParent,
            
        };
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
    
    public async Task<BackComment?> EditComment(int commentId, string newText)
    {
        var comment = await _context.Comments.FindAsync(commentId);
        if (comment == null) return null;
        comment.Text = newText;
        await _context.SaveChangesAsync();
        return new BackComment
        {
            Id = comment.Id,
            Text =  comment.Text,
            ImageUrl =  comment.ImageUrl,
            DateCreated =  comment.DateCreated,
            dislikes = comment.dislikes,
            likes = comment.likes,
            UserId = comment.UserId,
            postId = comment.IdPost,
            commentId = comment.IdCommentParent,
            
        };;
    }

    public async Task<List<BackComment>> GetCommentsByCommentId(int id)
    {
        return await _context.Comments
            .Where(c => c.IdCommentParent == id && c.IdPost == null).Select(c => new BackComment
            {
                    Id = c.Id,
                    Text =  c.Text,
                    ImageUrl =  c.ImageUrl,
                    DateCreated =  c.DateCreated,
                    dislikes = c.dislikes,
                    likes = c.likes,
                    UserId = c.UserId,
                    postId = c.IdPost,
                    commentId = c.IdCommentParent,
            }).ToListAsync();
    }
    public async Task<List<BackComment>> GetCommentsByPostId(int id)
    {
        return await _context.Comments
            .Where(c => c.IdPost == id && c.IdCommentParent == null).Select(c => new BackComment
            {
                Id = c.Id,
                Text =  c.Text,
                ImageUrl =  c.ImageUrl,
                DateCreated =  c.DateCreated,
                dislikes = c.dislikes,
                likes = c.likes,
                UserId = c.UserId,
                postId = c.IdPost,
                commentId = c.IdCommentParent,
            }).ToListAsync();
    }

    public async Task<List<BackComment>> GetCommentByUserId(int id)
    {
        return await _context.Comments.Where(c => c.UserId == id).Select(c => new BackComment
        {
            Id = c.Id,
            Text =  c.Text,
            ImageUrl =  c.ImageUrl,
            DateCreated =  c.DateCreated,
            dislikes = c.dislikes,
            likes = c.likes,
            UserId = c.UserId,
            postId = c.IdPost,
            commentId = c.IdCommentParent,
        }).ToListAsync();
    }
}