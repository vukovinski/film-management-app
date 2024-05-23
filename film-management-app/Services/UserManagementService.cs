namespace film_management_app.Server
{
    public class UserManagementService : IUserManagementService
    {
        private readonly IUserRepository _userRepository;

        public UserManagementService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void CreateActorUser(User actor)
        {
            actor.IsActor = true;
            _userRepository.CreateNew(actor);
        }

        public void CreateDirectorUser(User director)
        {
            director.IsDirector = true;
            _userRepository.CreateNew(director);
        }

        public void EditUser(User user)
        {
            _userRepository.Update(user);
        }
    }
}
