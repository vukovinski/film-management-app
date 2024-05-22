using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
    public List<Genre> Genres { get; set; } = new();
    public FilmDirector Director { get; set; }
    public List<FilmStar> Actors { get; set; } = new();
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

    public User User { get; set; }
    public Film Film { get; set; }
}

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
}
