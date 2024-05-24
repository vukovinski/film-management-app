using Microsoft.AspNetCore.Mvc;
using film_management_app.Server;
using Microsoft.AspNetCore.Authorization;

namespace film_management_app.Controllers;

[ApiController]
[Route("[controller]")]
public class SuperUserController : BaseAuthController
{
    private readonly IGenreRepository _genreRepository;
    private readonly IUserManagementService _managementService;

    public SuperUserController(IUserManagementService managementService, IGenreRepository genreRepository, IUserRepository userRepository) : base(userRepository)
    {
        _managementService = managementService;
        _genreRepository = genreRepository;
    }

    [HttpPost]
    [Authorize]
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
    [Authorize]
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
    [Authorize]
    public IEnumerable<GenreDto> GetGenres()
    {
        return _genreRepository.GetAll().Select(g => new GenreDto { Id = g.Id, Name = g.Name });
    }

    [HttpPost]
    [Authorize]
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
    [Authorize]
    public IActionResult UpdateGenre(GenreDto genre)
    {
        var genreD = _genreRepository.GetById(genre.Id);
        genreD.Name = genre.Name;
        _genreRepository.Update(genreD);
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public IActionResult DeleteGenre(int genreId)
    {
        _genreRepository.Delete(_genreRepository.GetById(genreId));
        return Ok();
    }
}
