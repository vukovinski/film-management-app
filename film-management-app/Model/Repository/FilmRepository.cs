﻿
using Microsoft.EntityFrameworkCore;

namespace film_management_app.Server
{
    public class FilmRepository : IFilmRepository
    {
        private readonly FilmManagementDbContext _context;
        public FilmRepository(FilmManagementDbContext context) => _context = context;

        public int CreateNew(Film film)
        {
            _context.Add(film);
            return _context.SaveChanges();
        }

        public int Delete(Film film)
        {
            _context.Remove(film);
            return _context.SaveChanges();
        }

        public IEnumerable<Film> GetByActor(int actorId)
        {
            return _context.Films
                .Where(f => f.Actors.Any(a => a.UserId == actorId))
                .Include(f => f.Genres)
                .ThenInclude(fg => fg.Genre)
                .Include(f => f.FeeNegotiations)
                .Include(f => f.Actors)
                .ThenInclude(fs => fs.User)
                .Include(f => f.Director)
                .ThenInclude(fd => fd!.User);
        }

        public IEnumerable<Film> GetByActor(FilmStar actor)
        {
            return _context.Films
                .Where(f => f.Actors.Any(a => a.UserId == actor.UserId))
                .Include(f => f.Genres)
                .ThenInclude(fg => fg.Genre)
                .Include(f => f.FeeNegotiations)
                .Include(f => f.Actors)
                .ThenInclude(fs => fs.User)
                .Include(f => f.Director)
                .ThenInclude(fd => fd!.User);
        }

        public IEnumerable<Film> GetByActor(User user)
        {
            return _context.Films
                .Where(f => f.Actors.Any(a => a.UserId == user.Id))
                .Include(f => f.Genres)
                .ThenInclude(fg => fg.Genre)
                .Include(f => f.FeeNegotiations)
                .Include(f => f.Actors)
                .ThenInclude(fs => fs.User)
                .Include(f => f.Director)
                .ThenInclude(fd => fd!.User);
        }

        public IEnumerable<Film> GetByDirector(int directorId)
        {
            return _context.Films
                .Include(f => f.Genres)
                .ThenInclude(fg => fg.Genre)
                .Include(f => f.FeeNegotiations)
                .Include(f => f.Actors)
                .ThenInclude(fs => fs.User)
                .Include(f => f.Director)
                .ThenInclude(fd => fd!.User)
                .Where(f => f.Director.UserId == directorId);
        }

        public IEnumerable<Film> GetByDirector(FilmDirector director)
        {
            return _context.Films
                .Include(f => f.Genres)
                .ThenInclude(fg => fg.Genre)
                .Include(f => f.FeeNegotiations)
                .Include(f => f.Actors)
                .ThenInclude(fs => fs.User)
                .Include(f => f.Director)
                .ThenInclude(fd => fd!.User)
                .Where(f => f.Director.UserId == director.UserId);
        }

        public IEnumerable<Film> GetByDirector(User user)
        {
            return _context.Films
                .Include(f => f.Genres)
                .ThenInclude(fg => fg.Genre)
                .Include(f => f.FeeNegotiations)
                .Include(f => f.Actors)
                .ThenInclude(fs => fs.User)
                .Include(f => f.Director)
                .ThenInclude(fd => fd!.User)
                .Where(f => f.Director.UserId == user.Id);
        }

        public IEnumerable<Film> GetByGenre(params int[] genreIds)
        {
            return _context.Films
                .Where(f => f.Genres.Select(g => g.GenreId).Union(genreIds).Count() == genreIds.Count())
                .Include(f => f.Genres)
                .ThenInclude(fg => fg.Genre)
                .Include(f => f.FeeNegotiations)
                .Include(f => f.Actors)
                .ThenInclude(fs => fs.User)
                .Include(f => f.Director)
                .ThenInclude(fd => fd!.User);
        }

        public IEnumerable<Film> GetByGenre(params Genre[] genres)
        {
            return _context.Films
                .Where(f => f.Genres.Select(g => g.GenreId).Union(genres.Select(g => g.Id)).Count() == genres.Count())
                .Include(f => f.Genres)
                .ThenInclude(fg => fg.Genre)
                .Include(f => f.FeeNegotiations)
                .Include(f => f.Actors)
                .ThenInclude(fs => fs.User)
                .Include(f => f.Director)
                .ThenInclude(fd => fd!.User);
        }

        public Film GetById(int id)
        {
            return _context.Films
                .Where(f => f.Id == id)
                .Include(f => f.Genres)
                .ThenInclude(fg => fg.Genre)
                .Include(f => f.FeeNegotiations)
                .Include(f => f.Actors)
                .ThenInclude(fs => fs.User)
                .Include(f => f.Director)
                .ThenInclude(fd => fd!.User)
                .First();
        }

        public IEnumerable<Film> GetByInvitedActor(User actor)
        {
            return _context.Films
                .Include(f => f.Actors)
                .ThenInclude(fs => fs.User)
                .Where(f => f.Actors.Any(a => a.UserId == actor.Id && !a.AcceptedRole))
                .Include(f => f.Genres)
                .ThenInclude(fg => fg.Genre)
                .Include(f => f.FeeNegotiations)
                .Include(f => f.Director)
                .ThenInclude(fd => fd!.User);
        }

        public IEnumerable<Film> GetByNoActor(int actorId)
        {
            return _context.Films
                .Include(f => f.Actors)
                .ThenInclude(fs => fs.User)
                .Where(f => !f.Actors.Any(a => a.UserId == actorId))
                .Include(f => f.Genres)
                .ThenInclude(fg => fg.Genre)
                .Include(f => f.FeeNegotiations)
                .Include(f => f.Director)
                .ThenInclude(fd => fd!.User);
        }

        public IEnumerable<Film> GetByShootingDate(DateTime from, DateTime to)
        {
            return _context.Films
                .Where(f => f.PlannedShootingStartDate > from && f.PlannedShootingEndDate <= to)
                .Include(f => f.Genres)
                .ThenInclude(fg => fg.Genre)
                .Include(f => f.FeeNegotiations)
                .Include(f => f.Actors)
                .ThenInclude(fs => fs.User)
                .Include(f => f.Director)
                .ThenInclude(fd => fd!.User);
        }

        public IEnumerable<Film> GetByTitle(string title)
        {
            return _context.Films
                .Where(f => f.Title.Contains(title))
                .Include(f => f.Genres)
                .ThenInclude(fg => fg.Genre)
                .Include(f => f.FeeNegotiations)
                .Include(f => f.Actors)
                .ThenInclude(fs => fs.User)
                .Include(f => f.Director)
                .ThenInclude(fd => fd!.User);
        }

        public int Update(Film film)
        {
            _context.Update(film);
            return _context.SaveChanges();
        }
    }
}
