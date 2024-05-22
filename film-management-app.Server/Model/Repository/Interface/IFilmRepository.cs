namespace film_management_app.Server
{
    public interface IFilmRepository
    {
        Film GetById(int id);
        IEnumerable<Film> GetByTitle(string title);
        IEnumerable<Film> GetByActor(int actorId);
        IEnumerable<Film> GetByActor(FilmStar actor);
        IEnumerable<Film> GetByActor(User user);
        IEnumerable<Film> GetByDirector(int directorId);
        IEnumerable<Film> GetByDirector(FilmDirector director);
        IEnumerable<Film> GetByDirector(User user);
        IEnumerable<Film> GetByGenre(params int[] genreIds);
        IEnumerable<Film> GetByGenre(params Genre[] genres);
        IEnumerable<Film> GetByShootingDate(DateTime from, DateTime to);

        int Update(Film film);
        int CreateNew(Film film);
        int Delete(Film film);
    }
}
