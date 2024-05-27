
namespace film_management_app.Server
{
    public class DirectorFilmsService : IDirectorFilmsService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly FilmManagementDbContext _context;

        public DirectorFilmsService(IFilmRepository filmRepository, FilmManagementDbContext context)
        {
            _context = context;
            _filmRepository = filmRepository;
        }

        public Film Create(string title, string tagLine, decimal budget, Genre[] genres, int directorId, DateTime plannedShootingStart, DateTime plannedShootingEnd)
        {
            var film = new Film
            {
                Title = title,
                TagLine = tagLine,
                Budget = budget,
                Genres = new List<FilmGenre>(),
                PlannedShootingStartDate = plannedShootingStart,
                PlannedShootingEndDate = plannedShootingEnd
            };
            _filmRepository.CreateNew(film);
            film.Director = new FilmDirector { Film = film, UserId = directorId };
            film.Genres = genres.Select(g => new FilmGenre { FilmId = film.Id, GenreId = g.Id }).ToList();
            _filmRepository.Update(film);
            return film;
        }

        public void Delete(Film film)
        {
            _filmRepository.Delete(film);
        }

        public void Edit(Film film)
        {
            _filmRepository.Update(film);
        }

        public Film[] MyFilms(User director)
        {
            return _filmRepository.GetByDirector(director).ToArray();
        }

        public User[] Actors()
        {
            return _context.Users.Where(u => u.IsActor).ToArray();
        }
    }
}
