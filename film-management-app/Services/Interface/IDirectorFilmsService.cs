namespace film_management_app.Server
{
    public interface IDirectorFilmsService
    {
        Film Create(
            string title,
            string tagLine,
            decimal budget,
            Genre[] genres,
            int directorId,
            DateTime plannedShootingStart,
            DateTime plannedShootingEnd
        );
        void Delete(Film film);
        void Edit(Film film);

        Film[] MyFilms(User director);

        User[] Actors();
    }
}
