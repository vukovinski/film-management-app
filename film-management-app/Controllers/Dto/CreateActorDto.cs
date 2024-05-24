using System.Text.Json.Serialization;

namespace film_management_app.Controllers;

public class CreateActorDto
{
    public string Email { get; set; }
    public decimal ExpectedFee { get; set; }
    public string FullName { get; set; }
    public string PasswordHash { get; set; }
}
