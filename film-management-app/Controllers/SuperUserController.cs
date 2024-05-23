using Microsoft.AspNetCore.Mvc;
using film_management_app.Server;

namespace film_management_app.Controllers;

[ApiController]
[Route("[controller]")]
public class SuperUserController : ControllerBase
{
    private readonly IGenreRepository _genreRepository;
    private readonly IUserManagementService _managementService;

    public SuperUserController(IUserManagementService managementService, IGenreRepository genreRepository)
    {
        _managementService = managementService;
        _genreRepository = genreRepository;
    }

    [HttpPost]
    public IActionResult CreateDirector(CreateDirectorDto directorUserDto)
    {
        _managementService.CreateDirectorUser(new User
        {
            FullName = directorUserDto.FullName,
            Email = directorUserDto.Email,
            IsDirector = true,
            PasswordHash = directorUserDto.PasswordHash
        });
        return Ok();
    }

    [HttpPost]
    public IActionResult CreateActor(CreateActorDto actorUserDto)
    {
        _managementService.CreateActorUser(new User
        {
            FullName = actorUserDto.FullName,
            Email = actorUserDto.Email,
            IsActor = true,
            PasswordHash = actorUserDto.PasswordHash
        });
        return Ok();
    }

    [HttpGet]
    public IEnumerable<GenreDto> GetGenres()
    {
        return _genreRepository.GetAll().Select(g => new GenreDto { Id = g.Id, Name = g.Name });
    }

    [HttpPost]
    public IActionResult CreateGenre(GenreDto genre)
    {
        _genreRepository.CreateNew(new Genre
        {
            Id = genre.Id,
            Name = genre.Name,
        });
        return Ok();
    }

    [HttpPost]
    public IActionResult UpdateGenre(GenreDto genre)
    {
        var genreD = _genreRepository.GetById(genre.Id);
        genreD.Name = genre.Name;
        _genreRepository.Update(genreD);
        return Ok();
    }

    [HttpDelete]
    public IActionResult DeleteGenre(int genreId)
    {
        _genreRepository.Delete(_genreRepository.GetById(genreId));
        return Ok();
    }
}
