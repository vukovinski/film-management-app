﻿namespace film_management_app.Controllers;

public class CreateActorDto
{
    public string FullName { get; internal set; }
    public string Email { get; internal set; }
    public string PasswordHash { get; internal set; }
}
