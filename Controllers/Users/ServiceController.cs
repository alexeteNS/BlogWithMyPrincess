using BlogWithMyPrincess.Dtos.Users.updates;
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
    
    [HttpPatch("Update/Email")]
    public async Task<IActionResult> Email([FromBody] email dto)
    {
        var i = await _userServer.UpdateEmail(dto.userId, dto.userEmail);
        return Ok(i);
    }
    [HttpPatch("Update/Password")]
    public async Task<IActionResult> Password([FromBody] password dto)
    {
        var i = await _userServer.UpdatePassword(dto.userId,dto.oldPassword, dto.newPassword);
        return Ok(i);
    }
    [HttpPatch("Update/Username")]
    public async Task<IActionResult> Username([FromBody] username dto)
    {
        var i = await _userServer.UpdateUsername(dto.userId,dto.userName);
        return Ok(i);
    }
}