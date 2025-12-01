using System.Security.Claims;
using BlogWithMyPrincess.Dtos.Users;
using BlogWithMyPrincess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWithMyPrincess.Controllers.Users;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserServices _userService;

    public AuthController(IUserServices userService)
    {
        _userService = userService;
    }
    
    [Authorize]
    [HttpGet("loginWithAuth")]
    public async Task<IActionResult> LoginWithAuth()
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var user = await _userService.LoginUserWithToken(userId);
    
        return Ok(user);
    }
    
    [HttpPost("loginWithData")]
    public async Task<IActionResult>  LoginWithData ([FromBody] Login Dto)
    {
        var user = await _userService.LoginUser(Dto.Email, Dto.Password);
        if (user == null) return BadRequest("No se pudo crear el usuario");
        return Ok(user);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> CreateUser([FromBody] Register Dto)
    {
        var user = await _userService.CreateUser(Dto.Username, Dto.Password, Dto.Email); 
        if (user == null) return BadRequest("No se pudo crear el usuario");
        return Ok(user);
    }


}