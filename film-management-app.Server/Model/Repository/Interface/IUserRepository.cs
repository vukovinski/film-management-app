namespace film_management_app.Server
{
    public interface IUserRepository
    {
        User GetByUserId(int userId);
        User GetByEmail(string email);
        IEnumerable<User> GetByFullName(string fullName);

        int Update(User user);
        int CreateNew(User user);
        int Delete(User user);
    }
}
