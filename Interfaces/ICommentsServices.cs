using BlogWithMyPrincess.Entities;

namespace BlogWithMyPrincess.Interfaces;

public interface ICommentsServices
{
    public Task<Comments?> CreateComment(string content, int? idPost, int? idComment, string? image, int userId);
    public Task<bool> DeleteComment(int commentId);
    public Task<Comments?> EditComment(int commentId, string newText);
    public Task<List<Comments>> GetCommentsByCommentId(int id);
    public Task<List<Comments>> GetCommentsByPostId(int id);
}