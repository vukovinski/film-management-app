namespace film_management_app.Controllers;

public class FilmDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string TagLine { get; set; }
    public decimal Budget { get; set; }
    public List<GenreDto> Genres { get; set; } = new();
    public DirectorDto Director { get; set; }
    public List<StarDto> Actors { get; set; } = new();
    public List<FeeNegotiationDto> Negotiations { get; set; } = new();
    public string PlannedShootingStartDate { get; set; }
    public string PlannedShootingEndDate { get; set; }

    public bool? HasBeenFilmed { get; set; } = null;
    public bool? IsOverBudget { get; set; }
    public bool? IsShootable { get; set; }
}
