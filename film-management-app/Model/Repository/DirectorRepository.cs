namespace film_management_app.Server
{
    public class DirectorRepository : IDirectorRepository
    {
        private readonly FilmManagementDbContext _context;
        public DirectorRepository(FilmManagementDbContext context) => _context = context;

        public int CreateNew(User director)
        {
            _context.Add(director);
            return _context.SaveChanges();
        }

        public int CreateNew(FilmDirector filmDirector)
        {
            _context.Add(filmDirector);
            return _context.SaveChanges();
        }

        public int Delete(User director)
        {
            _context.Remove(director);
            return _context.SaveChanges();
        }

        public int Delete(FilmDirector filmDirector)
        {
            _context.Remove(filmDirector);
            return _context.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            return _context.FilmDirectors.Where(fd => fd.User.Email == email).Select(fd => fd.User).Single();
        }

        public User GetByFilmId(int filmId)
        {
            return _context.FilmDirectors.Where(fd => fd.FilmId == filmId).Select(fd => fd.User).Single();
        }

        public IEnumerable<User> GetByFullName(string fullName)
        {
            return _context.FilmDirectors.Where(fd => fd.User.FullName.Contains(fullName)).Select(fd => fd.User);
        }

        public User GetByUserId(int userId)
        {
            return _context.FilmDirectors.Where(fd => fd.UserId == userId).Select(fd => fd.User).Single();
        }

        public int Update(User director)
        {
            _context.Update(director);
            return _context.SaveChanges();
        }

        public int Update(FilmDirector filmDirector)
        {
            _context.Update(filmDirector);
            return _context.SaveChanges();
        }
    }
}
