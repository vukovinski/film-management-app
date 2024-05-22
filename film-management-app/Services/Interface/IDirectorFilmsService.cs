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

        Film[] MyFilms();

        void Edit(Film film);
    }
}
