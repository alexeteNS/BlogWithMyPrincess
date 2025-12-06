using BlogWithMyPrincess.Dtos.Comment;
using BlogWithMyPrincess.Entities;

namespace BlogWithMyPrincess.Interfaces;

public interface ICommentsServices
{
    public Task<BackComment?> CreateComment(string content, int? idPost, int? idComment, string? image, int userId);
    public Task<bool> DeleteComment(int commentId);
    public Task<BackComment?> EditComment(int commentId, string newText);
    public Task<List<BackComment>> GetCommentsByCommentId(int id);
    public Task<List<BackComment>> GetCommentsByPostId(int id);
    public Task<List<BackComment>> GetCommentByUserId(int id);
}
