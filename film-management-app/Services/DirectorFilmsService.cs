
namespace film_management_app.Server
{
    public class DirectorFilmsService : IDirectorFilmsService
    {
        private readonly IFilmRepository _filmRepository;

        public DirectorFilmsService(IFilmRepository filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public Film Create(string title, string tagLine, decimal budget, Genre[] genres, FilmDirector director, DateTime plannedShootingStart, DateTime plannedShootingEnd)
        {
            var film = new Film
            {
                Title = title,
                TagLine = tagLine,
                Budget = budget,
                Genres = genres.ToList(),
                Director = director,
                PlannedShootingStartDate = plannedShootingStart,
                PlannedShootingEndDate = plannedShootingEnd
            };
            _filmRepository.CreateNew(film);
            return film;
        }

        public void Edit(Film film)
        {
            _filmRepository.Update(film);
        }

        public Film[] MyFilms(User director)
        {
            return _filmRepository.GetByDirector(director).ToArray();
        }
    }
}
