namespace film_management_app.Server
{
    public class GenreRepository : IGenreRepository
    {
        private readonly FilmManagementDbContext _context;
        public GenreRepository(FilmManagementDbContext context) => _context = context;

        public int CreateNew(Genre genre)
        {
            _context.Add(genre);
            return _context.SaveChanges();
        }

        public int Delete(Genre genre)
        {
            _context.Remove(genre);
            return _context.SaveChanges();
        }

        public Genre[] GetAll()
        {
            return _context.Genres.ToArray();
        }

        public Genre GetById(int genreId)
        {
            return _context.Genres.Where(g => g.Id == genreId).Single();
        }

        public int Update(Genre genre)
        {
            _context.Update(genre);
            return _context.SaveChanges();
        }
    }
}
