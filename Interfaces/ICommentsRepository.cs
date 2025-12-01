using BlogWithMyPrincess.Entities;

namespace BlogWithMyPrincess.Interfaces;

public interface ICommentsRepository
{
    public Task<Comments?> CreateComment(Comments? comments);
    public Task<bool> DeleteComment(int commentId);
    public Task<Comments?> EditComment(int commentId, string newText);
    public Task<List<Comments>> GetCommentsByCommentId(int id);
    public Task<List<Comments>> GetCommentsByPostId(int id);
    public Task<List<Comments>> GetCommentByUserId(int id);
}