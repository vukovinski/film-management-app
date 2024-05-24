using Microsoft.AspNetCore.Mvc;
using film_management_app.Server;
using Microsoft.AspNetCore.Authorization;

namespace film_management_app.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : BaseAuthController
{
    public ActorController(IUserRepository userRepository) : base(userRepository)
    {
        if (AuthenticatedUser == null || !AuthenticatedUser.IsActor)
            throw new BadHttpRequestException("User is not actor", 301);
    }

    [HttpPost]
    [Authorize]
    public IActionResult UpdateInfo(ActorDto actor)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Authorize]
    public ActorDto MyInfo()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Authorize]
    public IEnumerable<FilmDto> MyMovies()
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Authorize]
    public IActionResult AcceptInvite(int filmId)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Authorize]
    public IActionResult DeclineInvite(int filmId)
    {
        throw new NotImplementedException();
    }
}
