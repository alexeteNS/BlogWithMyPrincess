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
    
    public async Task<bool> CreateComment(Comments? comments)
    {
       return await _commentsRepository.CreateComment(comments);
    }

    public async Task<bool> DeleteComment(int commentId)
    {
        return await _commentsRepository.DeleteComment(commentId);
    }

    public async Task<Comments?> UpdateComment(int commentId, string newText)
    {
        return await _commentsRepository.UpdateComment(commentId, newText);
    }

    public async Task<List<Comments>> GetCommentsByCommentId(int id)
    {
        return await _commentsRepository.GetCommentsByCommentId(id);
    }

    public async Task<List<Comments>> GetCommentsByPostId(int id)
    {
        return await _commentsRepository.GetCommentsByPostId(id);
    }
}