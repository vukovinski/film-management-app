using Microsoft.AspNetCore.Mvc;
using film_management_app.Server;
using Microsoft.AspNetCore.Authorization;

namespace film_management_app.Controllers;

[ApiController]
[Route("[controller]")]
public class DirectorController : BaseAuthController
{
    private readonly IGenreRepository _genreRepository;
    private readonly IDirectorFilmsService _filmsService;
    private readonly IFeeNegotiationService _negotiationService;
    private readonly IActorInvitationService _invitationService;

    public DirectorController(IUserRepository userRepository, IDirectorFilmsService filmsService, IActorInvitationService invitationService, IHttpContextAccessor contextAccessor, IGenreRepository genreRepository, IFeeNegotiationService negotiateService) : base(userRepository, contextAccessor)
    {
        _filmsService = filmsService;
        _genreRepository = genreRepository;
        _negotiationService = negotiateService;
        _invitationService = invitationService;

        if (AuthenticatedUser == null || !AuthenticatedUser.IsDirector)
            throw new BadHttpRequestException("User not a director", 301);
    }

    [HttpGet]
    [Authorize]
    [Route("MyMovies")]
    public IEnumerable<FilmDto> MyMovies()
    {
        return _filmsService.MyFilms(AuthenticatedUser!).Select(f => new FilmDto
        {
            Id = f.Id,
            Title = f.Title,
            TagLine = f.TagLine,
            Budget = f.Budget,
            Genres = f.Genres.Select(g => new GenreDto { Id = g.GenreId, Name = _genreRepository.GetById(g.GenreId).Name }).ToList(),
            Director = new DirectorDto { Id = AuthenticatedUser!.Id, FullName = AuthenticatedUser!.FullName },
            Actors = f.Actors.Select(a => new ActorDto { Id = a.UserId, FullName = a.User.FullName, CurrentFee = a.Fee }).ToList(),
            Negotiations = f.FeeNegotiations.Select(fn => new FeeNegotiationDto { ActorId = fn.UserId, OldFee = fn.OldFee, NewFee = fn.NewFee }).ToList(),
            HasBeenFilmed = f.HasBeenFilmed,
            PlannedShootingStartDate = f.PlannedShootingStartDate.ToShortDateString(),
            PlannedShootingEndDate = f.PlannedShootingEndDate.ToShortDateString(),
            IsShootable = f.IsShootable,
            IsOverBudget = f.IsOverBudget
        });
    }

    [HttpGet]
    [Authorize]
    public IEnumerable<StarDto> MovieActors(int filmId)
    {
        var film = _filmsService.MyFilms(AuthenticatedUser!).First(f => f.Id == filmId);
        return film.Actors.Select(a => new StarDto
        {
            Fee = a.Fee,
            ActorId = a.UserId,
            FullName = a.User.FullName,
            AcceptedRole = a.AcceptedRole,
        });
    }

    [HttpGet]
    [Authorize]
    [Route("AllActors")]
    public IEnumerable<ActorDto> AllActors()
    {
        return _filmsService.Actors().Select(a => new ActorDto
        {
            Id = a.Id,
            FullName = a.FullName,
            CurrentFee = a.ExpectedFee ?? 1000m
        });
    }

    [HttpPut]
    [Authorize]
    [Route("InviteActor/{filmId}/{actorId}")]
    public StarDto InviteActor(int filmId, int actorId)
    {
        var film = _filmsService.MyFilms(AuthenticatedUser!).First(f => f.Id == filmId);
        var actor = _userRepository.GetByUserId(actorId);
        _invitationService.Invite(film, actor);

        return new StarDto
        {
            ActorId = actor.Id,
            AcceptedRole = false,
            FullName = actor.FullName,
            Fee = actor.ExpectedFee ?? 1000m,
        };
    }

    [HttpGet]
    [Authorize]
    [Route("MovieDetails/{filmId}")]
    public FilmDto MovieDetails(int filmId)
    {
        var film = _filmsService.MyFilms(AuthenticatedUser!).First(f => f.Id == filmId);
        return new FilmDto
        {
            Id = film.Id,
            Title = film.Title,
            TagLine = film.TagLine,
            Budget = film.Budget,
            Genres = film.Genres.Select(g => new GenreDto { Id = g.GenreId, Name = _genreRepository.GetById(g.GenreId).Name }).ToList(),
            Director = new DirectorDto { Id = AuthenticatedUser!.Id, FullName = AuthenticatedUser!.FullName },
            Actors = film.Actors.Select(a => new ActorDto { Id = a.UserId, FullName = a.User.FullName, CurrentFee = a.Fee }).ToList(),
            Negotiations = film.FeeNegotiations.Select(fn => new FeeNegotiationDto { ActorId = fn.UserId, OldFee = fn.OldFee, NewFee = fn.NewFee }).ToList(),
            HasBeenFilmed = film.HasBeenFilmed,
            PlannedShootingStartDate = film.PlannedShootingStartDate.ToString("yyyy-MM-dd"),
            PlannedShootingEndDate = film.PlannedShootingEndDate.ToString("yyyy-MM-dd"),
            IsShootable = film.IsShootable,
            IsOverBudget = film.IsOverBudget
        };
    }

    [HttpPost]
    [Authorize]
    [Route("CreateMovie")]
    public IActionResult CreateMovie(CreateFilmDto request)
    {
        _filmsService.Create(
            request.Title,
            request.TagLine,
            request.Budget,
            request.Genres.Select(g => new Genre { Id = (int)g.Id!, Name = g.Name }).ToArray(),
            AuthenticatedUser!.Id,
            DateOnly.Parse(request.PlannedShootingStartDate).ToDateTime(TimeOnly.MinValue),
            DateOnly.Parse(request.PlannedShootingEndDate).ToDateTime(TimeOnly.MinValue)
        );
        return Ok();
    }

    [HttpPost]
    [Authorize]
    [Route("EditMovie")]
    public IActionResult EditMovie(FilmDto request)
    {
        var film = _filmsService.MyFilms(AuthenticatedUser!).First(f => f.Id == request.Id);
        film.Title = request.Title;
        film.TagLine = request.TagLine;
        film.Budget = request.Budget;
        film.Genres = request.Genres.Select(g => new FilmGenre { FilmId = film.Id, GenreId = g.Id }).ToList();

        // TODO: actors
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public IActionResult DeleteMovie(int filmId)
    {
        var film = _filmsService.MyFilms(AuthenticatedUser!).First(f => f.Id == filmId);
        _filmsService.Delete(film);
        return Ok();
    }
}
