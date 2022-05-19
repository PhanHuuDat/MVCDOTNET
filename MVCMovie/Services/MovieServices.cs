using Microsoft.EntityFrameworkCore;
using MVCMovie.Data;
using MVCMovie.Models;

namespace MVCMovie.Services
{
    public class MovieServices
    {
        private readonly MVCMovieContext _context;

        public MovieServices(MVCMovieContext context)
        {
            this._context = context;
        }
        public async Task<List<Movie>> GetAllAsync()
        {
            var movies = from m in _context.Movie
                         select m;
            return await movies.ToListAsync();
        }
        public bool IsHasValue()
        {
            if(_context.Movie == null)
            {
                return false;
            }
            return true;
        }
        public async Task<List<Movie>> GetMoviesByConditionsAsync(string searchString, string searchGenre)
        {           
            var movies = from m in _context.Movie
                         select m;
            if (!string.IsNullOrEmpty(searchString))
            {
                movies = movies.Where(s => s.Title!.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(searchGenre))
            {
                movies = movies.Where(x => x.Genre == searchGenre);
            }

            return await movies.ToListAsync();
            
        }
        public async Task AddMovieAsync(Movie movie)
        {
            await _context.AddAsync(movie);
            await _context.SaveChangesAsync();
        }
        public IQueryable<string> GetGenresQuery()
        {
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;
            return genreQuery;
        }
    }
}
