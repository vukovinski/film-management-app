using Microsoft.AspNetCore.Mvc;

namespace film_management_app.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : ControllerBase
{
    [HttpPost]
    public IActionResult UpdateInfo(ActorDto actor)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public ActorDto MyInfo()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public IEnumerable<FilmDto> MyMovies()
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public IActionResult AcceptInvite(int filmId)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public IActionResult DeclineInvite(int filmId)
    {
        throw new NotImplementedException();
    }
}
