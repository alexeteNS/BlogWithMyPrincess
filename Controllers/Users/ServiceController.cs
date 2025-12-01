using BlogWithMyPrincess.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogWithMyPrincess.Controllers.Users;


[ApiController]
[Route("User/[controller]")]
public class ServiceController : ControllerBase
{
    private readonly IUserServices _userServer;

    public ServiceController(IUserServices userServer)
    {
        _userServer = userServer;
    }
    [HttpGet("get/{userId}")]
    public async Task<IActionResult> BackUser(int userId) {
        var user = await _userServer.GetUserByUId(userId);
        if (user == null) return BadRequest("El usuario no fue encontrado");
        return Ok(user);
    }
}