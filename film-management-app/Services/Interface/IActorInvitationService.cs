namespace film_management_app.Server
{
    public interface IActorInvitationService
    {
        void Invite(Film film, User actor);
        void RequestToJoin(Film film, User actor, decimal fee);
    }
}
