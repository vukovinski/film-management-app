namespace film_management_app.Server
{
    public class ActorInvitationService : IActorInvitationService
    {
        private readonly IStarRepository _starRepository;

        public ActorInvitationService(IStarRepository starRepository)
        {
            _starRepository = starRepository;

        }

        public void Invite(Film film, User actor)
        {
            CheckNotAlreadyCast(film, actor);
            _starRepository.CreateNew(new FilmStar() { Film = film, User = actor, FilmId = film.Id, UserId = actor.Id, Fee = actor.ExpectedFee ?? 1000m });
        }

        public void RequestToJoin(Film film, User actor, decimal fee)
        {
            CheckNotAlreadyCast(film, actor);
            _starRepository.CreateNew(new FilmStar() { Film = film, User = actor, FilmId = film.Id, UserId = actor.Id, Fee = fee, AcceptedRole = true });
        }

        private static void CheckNotAlreadyCast(Film film, User actor)
        {
            if (film.Actors.Any(a => a.UserId == actor.Id))
                throw new InvalidOperationException("Actor already cast in film.");
        }
    }
}