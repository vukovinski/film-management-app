namespace film_management_app.Server
{
    public interface IDirectorFilmsService
    {
        Film Create(
            string title,
            string tagLine,
            decimal budget,
            Genre[] genres,
            FilmDirector director,
            DateTime plannedShootingStart,
            DateTime plannedShootingEnd
        );

        void Edit(Film film);

        Film[] MyFilms(User director);
    }
}
