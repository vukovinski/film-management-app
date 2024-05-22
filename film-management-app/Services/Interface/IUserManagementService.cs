namespace film_management_app.Server
{
    public interface IUserManagementService
    {
        void EditUser(User user);
        void CreateActorUser(User actor);
        void CreateDirectorUser(User director);
    }
}
