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

    public SuperUserController(IUserManagementService managementService, IGenreRepository genreRepository, IUserRepository userRepository, IHttpContextAccessor contextAccessor) : base(userRepository, contextAccessor)
    {
        _managementService = managementService;
        _genreRepository = genreRepository;
    }

    [HttpPost]
    [Authorize]
    [Route("CreateDirector")]
    public IActionResult CreateDirector([FromBody] CreateDirectorDto directorUserDto)
    {
        CheckEmailUnique(directorUserDto.Email);
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
    [Route("CreateActor")]
    public IActionResult CreateActor([FromBody] CreateActorDto actorUserDto)
    {
        CheckEmailUnique(actorUserDto.Email);
        _managementService.CreateActorUser(new User
        {
            FullName = actorUserDto.FullName,
            Email = actorUserDto.Email,
            IsActor = true,
            PasswordHash = actorUserDto.PasswordHash,
            ExpectedFee = actorUserDto.ExpectedFee
        });
        return Ok();
    }

    [HttpGet]
    [Authorize]
    [Route("GetGenres")]
    public IEnumerable<GenreDto> GetGenres()
    {
        return _genreRepository.GetAll().Select(g => new GenreDto { Id = g.Id, Name = g.Name });
    }

    [HttpPost]
    [Authorize]
    [Route("CreateGenre")]
    public IActionResult CreateGenre([FromBody] GenreDto genre)
    {
        CheckGenreUnique(genre.Name);
        _genreRepository.CreateNew(new Genre
        {
            Name = genre.Name,
        });
        return Ok();
    }

    [HttpPost]
    [Authorize]
    [Route("UpdateGenre")]
    public IActionResult UpdateGenre([FromBody] GenreDto genre)
    {
        var genreD = _genreRepository.GetById((int)genre.Id!);
        genreD.Name = genre.Name;
        _genreRepository.Update(genreD);
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    [Route("DeleteGenre/{genreId}")]
    public IActionResult DeleteGenre(int genreId)
    {
        _genreRepository.Delete(_genreRepository.GetById(genreId));
        return Ok();
    }

    private void CheckEmailUnique(string email)
    {
        var existingUser = _userRepository.GetByEmail(email);
        if (existingUser != null)
            throw new BadHttpRequestException("User with that email already exists.", 400);
    }

    private void CheckGenreUnique(string name)
    {
        var existingGenre = _genreRepository.GetAll().Where(g => g.Name.ToLower() == name.ToLower()).FirstOrDefault();
        if (existingGenre != null)
            throw new BadHttpRequestException("Genre with that name already exists.", 400);
    }
}
