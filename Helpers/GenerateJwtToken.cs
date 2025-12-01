using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BlogWithMyPrincess.Entities;
using Microsoft.IdentityModel.Tokens;

namespace BlogWithMyPrincess.Helpers;

public static class GenerateJwtToken
{
    public static string Generate(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("EstaEsUnaClaveMuySeguraYSegura123456!!");

        var tokenDescription = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
            }),
            Expires =  DateTime.UtcNow.AddDays(7),
            Issuer = "MiBlogAPI",
            Audience = "MiBlogClient",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),  SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }
    
}