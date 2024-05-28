using Microsoft.AspNetCore.Mvc;
using film_management_app.Server;
using Microsoft.AspNetCore.Authorization;

namespace film_management_app.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : BaseAuthController
{
    private readonly IStarRepository _starRepository;
    private readonly IActorFilmsService _filmsService;
    private readonly IGenreRepository _genreRepository;
    private readonly IFeeNegotiationService _negotiationService;

    public ActorController(IUserRepository userRepository, IGenreRepository genreRepository, IFeeNegotiationService feeNegotiationService, IActorFilmsService actorFilmsService, IStarRepository starRepository,  IHttpContextAccessor contextAccessor) : base(userRepository, contextAccessor)
    {
        _starRepository = starRepository;
        _filmsService = actorFilmsService;
        _genreRepository = genreRepository;
        _negotiationService = feeNegotiationService;

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
    [Route("MovieDetails/{movieId}")]
    public FilmDto MovieDetails(int movieId)
    {
        var f = _filmsService.GetById(movieId);
        return new FilmDto
        {
            Id = f.Id,
            Title = f.Title,
            TagLine = f.TagLine,
            Budget = f.Budget,
            Genres = f.Genres.Select(g => new GenreDto { Id = g.GenreId, Name = _genreRepository.GetById(g.GenreId).Name }).ToList(),
            Director = new DirectorDto { Id = f.Director!.UserId, FullName = f.Director.User.FullName },
            Actors = f.Actors.Select(a => new StarDto { ActorId = a.UserId, FullName = a.User.FullName, Fee = a.Fee, AcceptedRole = a.AcceptedRole }).ToList(),
            Negotiations = f.FeeNegotiations.Select(fn => new FeeNegotiationDto { ActorId = fn.UserId, OldFee = fn.OldFee, NewFee = fn.NewFee }).ToList(),
            HasBeenFilmed = f.HasBeenFilmed,
            PlannedShootingStartDate = f.PlannedShootingStartDate.ToShortDateString(),
            PlannedShootingEndDate = f.PlannedShootingEndDate.ToShortDateString(),
            IsShootable = f.IsShootable,
            IsOverBudget = f.IsOverBudget
        };
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
            Director = new DirectorDto { Id = f.Director!.UserId, FullName = f.Director.User.FullName },
            Actors = f.Actors.Select(a => new StarDto { ActorId = a.UserId, FullName = a.User.FullName, Fee = a.Fee, AcceptedRole = a.AcceptedRole }).ToList(),
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
    [Route("OtherMovies")]
    public IEnumerable<FilmDto> OtherMovies()
    {
        return _filmsService.OtherFilms(AuthenticatedUser!).Select(f => new FilmDto
        {
            Id = f.Id,
            Title = f.Title,
            TagLine = f.TagLine,
            Budget = f.Budget,
            Genres = f.Genres.Select(g => new GenreDto { Id = g.GenreId, Name = _genreRepository.GetById(g.GenreId).Name }).ToList(),
            Director = new DirectorDto { Id = f.Director!.UserId, FullName = f.Director.User.FullName },
            Actors = f.Actors.Select(a => new StarDto { ActorId = a.UserId, FullName = a.User.FullName, Fee = a.Fee, AcceptedRole = a.AcceptedRole }).ToList(),
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
    [Route("GetStarring/{filmId}")]
    public StarDto GetStarring(int filmId)
    {
        var film = _filmsService.MyFilms(AuthenticatedUser!).First(f => f.Id == filmId);
        var starring = film.Actors.First(a => a.UserId == AuthenticatedUser!.Id);
        return new StarDto
        {
            Fee = starring.Fee,
            ActorId = starring.UserId,
            FullName = starring.User.FullName,
            AcceptedRole = starring.AcceptedRole
        };
    }

    [HttpPut]
    [Authorize]
    [Route("ApplyForMovie/{filmId}/{fee}")]
    public IActionResult ApplyForMovie(int filmId, decimal fee)
    {
        var star = new FilmStar { FilmId = filmId, UserId = AuthenticatedUser!.Id, Fee = fee };
        _starRepository.CreateNew(star);
        return Ok();
    }

    [HttpPut]
    [Authorize]
    [Route("CreateNegotiation/{filmId}/{newFee}")]
    public IActionResult CreateNegotiation(int filmId, decimal newFee)
    {
        var film = _filmsService.MyFilms(AuthenticatedUser!).First(f => f.Id == filmId);
        var starring = film.Actors.First(a => a.UserId == AuthenticatedUser!.Id);
        _negotiationService.Create(starring, newFee);
        return Ok();
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
