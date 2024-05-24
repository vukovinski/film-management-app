using Microsoft.AspNetCore.Mvc;
using film_management_app.Server;
using Microsoft.AspNetCore.Authorization;

namespace film_management_app.Controllers;

[ApiController]
[Route("[controller]")]
public class DirectorController : BaseAuthController
{
    public DirectorController(IUserRepository userRepository) : base(userRepository)
    {
        if (AuthenticatedUser == null || !AuthenticatedUser.IsDirector)
            throw new BadHttpRequestException("User not a director", 301);
    }

    [HttpGet]
    [Authorize]
    public IEnumerable<FilmDto> MyMovies()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Authorize]
    public IEnumerable<StarDto> ActorsByMovie(int filmId)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    [Authorize]
    public StarDto InviteActor(int filmId, int actorId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    [Authorize]
    public FilmDto MovieDetails(int filmId)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateMovie(CreateFilmDto request)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    [Authorize]
    public IActionResult EditMovie(FilmDto request)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    [Authorize]
    public IActionResult DeleteMovie(int filmId)
    {
        throw new NotImplementedException();
    }
}
