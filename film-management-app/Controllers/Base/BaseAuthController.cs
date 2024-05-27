using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using film_management_app.Server;

namespace film_management_app;

public class BaseAuthController : ControllerBase
{
    protected readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _contextAccessor;

    public BaseAuthController(IUserRepository userRepository, IHttpContextAccessor contextAccessor)
    {
        _userRepository = userRepository;
        _contextAccessor = contextAccessor;
    }

    public User? AuthenticatedUser
    {
        get
        {
            var claim = _contextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.Email);
            if (claim != null) return _userRepository.GetByEmail(claim);
            return null;
        }
    }

    public bool? IsActor => AuthenticatedUser?.IsActor;
    public bool? IsDirector => AuthenticatedUser?.IsDirector;
}
