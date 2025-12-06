using BlogWithMyPrincess.Dtos.Comment;
using BlogWithMyPrincess.Entities;
using BlogWithMyPrincess.Interfaces;

namespace BlogWithMyPrincess.Services;

public class CommentServices : ICommentsServices
{
    private  readonly ICommentsRepository _commentsRepository;

    public CommentServices(ICommentsRepository commentsRepository)
    {
        _commentsRepository = commentsRepository;
    }
    
    public async Task<BackComment?> CreateComment(string content, int? idPost, int? idComment, string? image, int userId)
    {
        if(idPost == null && idComment == null) return null;
        if (idPost != null && idComment != null) return null;
        
        Comments comments = new Comments
        {
            Text =  content,
            IdPost = idPost,
            IdCommentParent =  idComment,
            ImageUrl = image,
            UserId =  userId,
            dislikes = 0,
            likes = 0,
        };
        
        return await _commentsRepository.CreateComment(comments);
    }

    public async Task<bool> DeleteComment(int commentId)
    {
        return await _commentsRepository.DeleteComment(commentId);
    }

    public async Task<BackComment?> EditComment(int commentId, string newText)
    {
        return await _commentsRepository.EditComment(commentId, newText);
    }

    public async Task<List<BackComment>> GetCommentsByCommentId(int id)
    {
        return await _commentsRepository.GetCommentsByCommentId(id);
    }

    public async Task<List<BackComment>> GetCommentByUserId(int id)
    {
        return await _commentsRepository.GetCommentByUserId(id);
    }

    public async Task<List<BackComment>> GetCommentsByPostId(int id)
    {
        return await _commentsRepository.GetCommentsByPostId(id);
    }
}