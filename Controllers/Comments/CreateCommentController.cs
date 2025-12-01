using BlogWithMyPrincess.Dtos.Comment;
using BlogWithMyPrincess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogWithMyPrincess.Controllers.Comments;

[ApiController]
[Route("api/[controller]")]
public class CommentController : ControllerBase
{
    private readonly  ICommentsServices _commentsServices;

    public CommentController(ICommentsServices commentsServices)
    {
        _commentsServices = commentsServices;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateComment([FromBody] CreateComment dto)
    {
        var comment = await _commentsServices.CreateComment(dto.Content, dto.PostId, dto.CommentId, dto.Image, dto.UserId);
        if(comment == null) return BadRequest("Comentario no creado");
        return Ok(comment);
    }
}