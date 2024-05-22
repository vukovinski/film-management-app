namespace film_management_app.Server
{
    public class StarRepository : IStarRepository
    {
        private readonly FilmManagementDbContext _context;
        public StarRepository(FilmManagementDbContext context) => _context = context;

        public int CreateNew(User actor)
        {
            _context.Add(actor);
            return _context.SaveChanges();
        }

        public int CreateNew(FilmStar filmStar)
        {
            _context.Add(filmStar);
            return _context.SaveChanges();
        }

        public int Delete(User actor)
        {
            _context.Remove(actor);
            return _context.SaveChanges();
        }

        public int Delete(FilmStar filmStar)
        {
            _context.Remove(filmStar);
            return _context.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            return _context.FilmStars.Where(fs => fs.User.Email.Contains(email)).Select(fs => fs.User).Single();
        }

        public IEnumerable<User> GetByFilmId(int filmId)
        {
            return _context.FilmStars.Where(fs => fs.FilmId == filmId).Select(fs => fs.User);
        }

        public IEnumerable<User> GetByFullName(string fullName)
        {
            return _context.FilmStars.Where(fs => fs.User.FullName.Contains(fullName)).Select(fs => fs.User);
        }

        public User GetByUserId(int userId)
        {
            return _context.FilmStars.Where(fs => fs.UserId == userId).Select(fs => fs.User).Single();
        }

        public int Update(User actor)
        {
            _context.Update(actor);
            return _context.SaveChanges();
        }

        public int Update(FilmStar filmStar)
        {
            _context.Update(filmStar);
            return _context.SaveChanges();
        }
    }
}
