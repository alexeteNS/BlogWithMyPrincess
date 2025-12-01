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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPost(int id)
    {
        var post =await _postService.GetPostById(id);
        if (post == null) return BadRequest("No se pudo obtener el post");
        return Ok(post);
    }

    [HttpPatch("edit")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] PostUpdate dto)
    {
        var post = await _postService.UpdatePost(dto.Content, dto.ImageUrl, dto.PostId);
        if (post == null) return BadRequest("No se pudo actualizar el post");
        return Ok(post);
    }
}