using BlogWithMyPrincess.Dtos.Post;
using BlogWithMyPrincess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogWithMyPrincess.Controllers.Posts;

[ApiController]
[Route("api/[controller]")]
public class ServicePostController : ControllerBase
{
    private readonly IPostService _postService;
    public ServicePostController(IPostService postService)
    {
        _postService = postService;
    }
    [HttpGet("Posts")]
    public async Task<List<BackPostInfo>> GetAllPosts()
    {
        return await _postService.GetAllPosts();
    }
    [HttpGet("Post/Author/{authorId}")]
    public async Task<List<BackPostInfo>> GetAllPostsByAuthor(int authorId)
    {
        return await _postService.GetAllPostsByAuthor(authorId);
    }
}