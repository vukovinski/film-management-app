namespace film_management_app.Server
{
    public interface IActorInvitationService
    {
        void Invite(Film film, User actor);
        void RequestToJoint(Film film, User actor, decimal fee);
    }
}
