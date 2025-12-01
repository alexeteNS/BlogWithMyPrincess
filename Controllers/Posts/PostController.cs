using BlogWithMyPrincess.Dtos.Post;
using BlogWithMyPrincess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogWithMyPrincess.Controllers.Posts;

[ApiController]
[Route("api/[controller]")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    public PostController(IPostService postService)
    {
        _postService = postService;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreatePost([FromBody] PostCreate dto)
    {
        var post = await _postService.CreatePost(dto.Content, dto.ImageUrl, dto.UserId);
        if (post == null) return BadRequest("No se pudo crear el post");
        return Ok("Post creado");
    }
}