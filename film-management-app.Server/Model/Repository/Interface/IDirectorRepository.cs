namespace film_management_app.Server
{
    public interface IDirectorRepository
    {
        User GetByEmail(string email);
        User GetByUserId(int userId);
        User GetByFilmId(int filmId);
        IEnumerable<User> GetByFullName(string fullName);

        int Update(User director);
        int Update(FilmDirector filmDirector);

        int CreateNew(User director);
        int CreateNew(FilmDirector filmDirector);

        int Delete(User director);
        int Delete(FilmDirector filmDirector);
    }
}
