namespace film_management_app.Server
{
    public interface IActorFilmsService
    {
        Film[] InvitedFilms(User actor);
        void UpdateInformation(User actor);
    }
}
