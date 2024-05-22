namespace film_management_app.Server
{
    public interface IActorFilmsService
    {
        Film[] InvitedFilms();
        void UpdateInformation(User actor);
    }
}
