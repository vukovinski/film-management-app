using Microsoft.AspNetCore.Mvc;

namespace film_management_app.Controllers;

[ApiController]
[Route("[controller]")]
public class DirectorController : ControllerBase
{
    [HttpGet]
    public IEnumerable<FilmDto> MyMovies()
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public IEnumerable<StarDto> ActorsByMovie(int filmId)
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public StarDto InviteActor(int filmId, int actorId)
    {
        throw new NotImplementedException();
    }

    [HttpGet]
    public FilmDto MovieDetails(int filmId)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public IActionResult CreateMovie(CreateFilmDto request)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public IActionResult EditMovie(FilmDto request)
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public IActionResult DeleteMovie(int filmId)
    {
        throw new NotImplementedException();
    }
}
