using BlogWithMyPrincess.Entities;

namespace BlogWithMyPrincess.Interfaces;

public interface ICommentsRepository
{
    public Task<bool> CreateComment(Comments? comments);
    public Task<bool> DeleteComment(int commentId);
    public Task<Comments?> UpdateComment(int commentId, string newText);
    public Task<List<Comments>> GetCommentsByCommentId(int id);
    public Task<List<Comments>> GetCommentsByPostId(int id);
}