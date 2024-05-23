namespace film_management_app.Server
{
    public interface IStarRepository
    {
        User GetByEmail(string email);
        User GetByUserId(int userId);
        IEnumerable<User> GetByFilmId(int filmId);
        IEnumerable<User> GetByFullName(string fullName);

        FilmStar GetFilmStarByEmail(string email);
        FilmStar GetFilmStarByUserId(int userId);
        FilmStar GetFilmStarByFilmIdAndUserId(int filmId, int userId);
        IEnumerable<FilmStar> GetFilmStarByFilmId(int filmId);
        IEnumerable<FilmStar> GetFilmStarByFullName(string fullName);

        int Update(User actor);
        int Update(FilmStar filmStar);

        int CreateNew(User actor);
        int CreateNew(FilmStar filmStar);

        int Delete(User actor);
        int Delete(FilmStar filmStar);
    }
}
