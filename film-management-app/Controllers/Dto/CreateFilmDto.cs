namespace film_management_app.Controllers;

public class CreateFilmDto
{
    public string Title { get; set; }
    public string TagLine { get; set; }
    public decimal Budget { get; set; }
    public List<GenreDto> Genres { get; set; } = new();
    public string PlannedShootingStartDate { get; set; }
    public string PlannedShootingEndDate { get; set; }
}