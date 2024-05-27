namespace film_management_app.Server
{
    public interface IActorFilmsService
    {
        Film[] MyFilms(User actor);
        Film[] InvitedFilms(User actor);
        void UpdateInformation(User actor);
    }
}
