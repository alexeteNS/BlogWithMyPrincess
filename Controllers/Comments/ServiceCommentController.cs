using BlogWithMyPrincess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogWithMyPrincess.Controllers.Comments;


[ApiController]
[Route("api/[controller]")]
public class ServiceCommentController : ControllerBase
{
    private readonly  ICommentsServices _commentsServices;

    public ServiceCommentController(ICommentsServices commentsServices)
    {
        _commentsServices = commentsServices;
    }
    [HttpGet("User/Id/{userId}")]
    public async Task<IActionResult> GetCommentsByUserId(int userId)
    {
        var comments =  await _commentsServices.GetCommentByUserId(userId);
        return Ok(comments);
    }

    [HttpGet("Post/Id/{postId}")]
    public async Task<IActionResult> GetCommentsByPostId(int postId)
    {
        var comments = await _commentsServices.GetCommentsByPostId(postId);
        return Ok(comments);
    }

    [HttpGet("Comment/Id/{commentId}")]
    public async Task<IActionResult> GetCommentByCommentId(int commentId)
    {
        var comments = await _commentsServices.GetCommentsByCommentId(commentId);
        return Ok(comments);
    }
    
}