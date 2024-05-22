namespace film_management_app.Server
{
    public interface IGenreRepository
    {
        Genre[] GetAll();
        Genre GetById(int genreId);
        int Update(Genre genre);
        int CreateNew(Genre genre);
        int Delete(Genre genre);
    }
}
