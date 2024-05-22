namespace film_management_app.Server
{
    public class FilmRepository : IFilmRepository
    {
        private readonly FilmManagementDbContext _context;
        public FilmRepository(FilmManagementDbContext context) => _context = context;

        public int CreateNew(Film film)
        {
            _context.Add(film);
            return _context.SaveChanges();
        }

        public int Delete(Film film)
        {
            _context.Remove(film);
            return _context.SaveChanges();
        }

        public IEnumerable<Film> GetByActor(int actorId)
        {
            return _context.Films.Where(f => f.Actors.Any(a => a.UserId == actorId));
        }

        public IEnumerable<Film> GetByActor(FilmStar actor)
        {
            return _context.Films.Where(f => f.Actors.Any(a => a.UserId == actor.UserId));
        }

        public IEnumerable<Film> GetByActor(User user)
        {
            return _context.Films.Where(f => f.Actors.Any(a => a.UserId == user.Id));
        }

        public IEnumerable<Film> GetByDirector(int directorId)
        {
            return _context.Films.Where(f => f.Director.UserId == directorId);
        }

        public IEnumerable<Film> GetByDirector(FilmDirector director)
        {
            return _context.Films.Where(f => f.Director.UserId == director.UserId);
        }

        public IEnumerable<Film> GetByDirector(User user)
        {
            return _context.Films.Where(f => f.Director.UserId == user.Id);
        }

        public IEnumerable<Film> GetByGenre(params int[] genreIds)
        {
            return _context.Films.Where(f => f.Genres.Select(g => g.Id).Union(genreIds).Count() == genreIds.Count());
        }

        public IEnumerable<Film> GetByGenre(params Genre[] genres)
        {
            return _context.Films.Where(f => f.Genres.Select(g => g.Id).Union(genres.Select(g => g.Id)).Count() == genres.Count());
        }

        public Film GetById(int id)
        {
            return _context.Films.Where(f => f.Id == id).First();
        }

        public IEnumerable<Film> GetByTitle(string title)
        {
            return _context.Films.Where(f => f.Title.Contains(title));
        }

        public int Update(Film film)
        {
            _context.Update(film);
            return _context.SaveChanges();
        }
    }
}
