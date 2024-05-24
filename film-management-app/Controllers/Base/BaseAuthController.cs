using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using film_management_app.Server;

namespace film_management_app;

public class BaseAuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public BaseAuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public User? AuthenticatedUser
    {
        get
        {
            var claim = User.FindFirstValue(ClaimTypes.Email);
            if (claim == null) return null;
            return _userRepository.GetByEmail(claim);
        }
    }

    public bool? IsActor => AuthenticatedUser?.IsActor;
    public bool? IsDirector => AuthenticatedUser?.IsDirector;
}
