namespace film_management_app.Server
{
    public class UserRepository : IUserRepository
    {
        private readonly FilmManagementDbContext _context;
        public UserRepository(FilmManagementDbContext context) => _context = context;

        public int CreateNew(User user)
        {
            _context.Add(user);
            return _context.SaveChanges();
        }

        public int Delete(User user)
        {
            _context.Remove(user);
            return _context.SaveChanges();
        }

        public User GetByEmail(string email)
        {
            return _context.Users.Where(u => u.Email == email).Single();
        }

        public IEnumerable<User> GetByFullName(string fullName)
        {
            return _context.Users.Where(u => u.FullName.Contains(fullName));
        }

        public User GetByUserId(int userId)
        {
            return _context.Users.Where(u => u.Id == userId).Single();
        }

        public int Update(User user)
        {
            _context.Update(user);
            return _context.SaveChanges();
        }
    }
}
