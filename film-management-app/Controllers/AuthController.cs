using Microsoft.AspNetCore.Mvc;
using film_management_app.Server;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace film_management_app.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Login([FromQuery]string email, [FromQuery]string passwordHash)
    {
        var user = _userRepository.GetByEmail(email);
        if (user == null)
            return Unauthorized();

        if (user.PasswordHash != passwordHash)
            return Unauthorized();

        return LoginUser(user);
    }

    [Authorize]
    [HttpGet]
    public IActionResult Logout()
    {
        var anonymousUser = new ClaimsPrincipal();
        SetPrincipal(anonymousUser);
        return Ok();
    }

    #region helpers
    private IActionResult LoginUser(User user)
    {
        var issuer = "https://localhost:44414";
        var audience = "https://localhost:44414";
        var key = Encoding.UTF8.GetBytes("991b2b979968afd96afdf5d737cb27f1cd98922f7bed756c3cfa6fdec2f276aa0121d2e8aaaf49d1e4a316a7d3954a1af7e74a67abb8e859e9a8d2c8a56f9f4e");
        var identity = new ClaimsIdentity(new[]
        {
            new Claim("Id", Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

            new Claim("IsActor", user.IsActor.ToString()),
            new Claim("IsDirector", user.IsDirector.ToString()),
        });
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = identity,
            Expires = DateTime.UtcNow.AddMinutes(5),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha512Signature
            )
        };
        var role = user.IsActor ? "actor" : user.IsDirector ? "director" : user.IsAdmin ? "admin" : "none";
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        var principal = new ClaimsPrincipal();
        principal.AddIdentity(identity);
        SetPrincipal(principal);
        return Ok(new { role = role, token = jwtToken});
    }

    private void SetPrincipal(ClaimsPrincipal principal)
    {
        Thread.CurrentPrincipal = principal;
        HttpContext.User = principal;
    }
    #endregion
}
