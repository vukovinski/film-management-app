using Microsoft.EntityFrameworkCore;

namespace film_management_app.Server;

public class FilmManagementDbContext : DbContext
{
    public DbSet<Film> Films { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<FilmStar> FilmStars { get; set; }
    public DbSet<FilmDirector> FilmDirectors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FilmDirector>()
            .HasKey(fd => new { fd.UserId, fd.FilmId });

        modelBuilder.Entity<FilmStar>()
            .HasKey(fs => new { fs.UserId, fs.FilmId });

        modelBuilder.Entity<FeeNegotiation>()
            .HasKey(fn => new { fn.UserId, fn.FilmId });

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=film-management-app.db");
    }
}

public class Film
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string TagLine { get; set; }
    public decimal Budget { get; set; }
    public FilmDirector? Director { get; set; }
    public List<FilmGenre> Genres { get; set; } = new();
    public List<FilmStar> Actors { get; set; } = new();
    public List<FeeNegotiation> FeeNegotiations { get; set; } = new();
    public bool HasBeenFilmed { get; set; } = false;

    public DateTime PlannedShootingStartDate { get; set; }
    public DateTime PlannedShootingEndDate { get; set; }

    public bool IsShootable => !HasBeenFilmed && !IsOverBudget;
    public bool IsOverBudget => Actors.Select(a => a.Fee).Sum() > Budget;
}

public class User
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string? PasswordHash { get; set; }
    public decimal? ExpectedFee { get; set; }


    public bool IsActor { get; set; } = false;
    public bool IsDirector { get; set; } = false;
    public bool HasEverLoggedIn { get; set; } = false;

    public bool IsAdmin { get; set; } = false;
}

public class FilmDirector
{
    public int UserId { get; set; }
    public int FilmId { get; set; }

    public User User { get; set; }
    public Film Film { get; set; }
}

public class FilmStar
{
    public int UserId { get; set; }
    public int FilmId { get; set; }
    public decimal Fee { get; set; }
    public bool AcceptedRole { get; set; }

    public User User { get; set; }
    public Film Film { get; set; }
}

public class FilmGenre
{
    public int Id { get; set; }
    public int FilmId { get; set; }
    public int GenreId { get; set; }

    public Film Film { get; set; }
    public Genre Genre { get; set; }
}

public class FeeNegotiation
{
    public int UserId { get; set; }
    public int FilmId { get; set; }

    public decimal OldFee { get; set; }
    public decimal NewFee { get; set; }
}

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
}
