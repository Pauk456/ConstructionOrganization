using Microsoft.AspNetCore.Mvc;
using Authorization.Models;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace Authorization.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AuthorizationController : Controller
{
    private readonly AppDbContext _context;

    public AuthorizationController(AppDbContext context)
    {
        _context = context;
    }

    public record UserDataDTO(string Login, string Password);

    [HttpPost("registration")]
    public async Task<IActionResult> Registration([FromBody] UserDataDTO userDataDTO)
    {
        _context.UserDatas.Add( new UserData 
        { 
            Login = userDataDTO.Login,
            Password = userDataDTO.Password
        });
        _context.SaveChanges();

        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authotization([FromBody] UserDataDTO userDataDTO)
    {
        var user = _context.UserDatas.FirstOrDefault(p => p.Login == userDataDTO.Login && p.Password == userDataDTO.Password);
        if (user is null) return BadRequest("SQL запрос не указан");

        var claims = new List<Claim> { new Claim(ClaimTypes.Name, userDataDTO.Login) };

        var jwt = new JwtSecurityToken(
            issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(2)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );


        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            access_token = encodedJwt,
            username = userDataDTO.Login
        };

        return Ok(response);
    }
    [Authorize]
    [HttpGet("data")]
    public async Task<IActionResult> Data()
    {
        return Ok("Succes");
    }
}
