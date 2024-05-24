namespace film_management_app.Controllers;

public class CreateDirectorDto
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}
