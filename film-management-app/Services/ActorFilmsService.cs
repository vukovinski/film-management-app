namespace film_management_app.Server
{
    public class ActorFilmsService : IActorFilmsService
    {
        private readonly IFilmRepository _filmRepository;
        private readonly IUserRepository _userRepository;

        public ActorFilmsService(IFilmRepository filmRepository, IUserRepository userRepository)
        {
            _filmRepository = filmRepository;
            _userRepository = userRepository;
        }

        public Film[] InvitedFilms(User actor)
        {
            return _filmRepository.GetByInvitedActor(actor).ToArray();
        }

        public Film[] MyFilms(User actor)
        {
            return _filmRepository.GetByActor(actor).ToArray();
        }

        public Film[] OtherFilms(User actor)
        {
            return _filmRepository.GetByNoActor(actor.Id).ToArray();
        }

        public void UpdateInformation(User actor)
        {
            _userRepository.Update(actor);
        }
    }
}
