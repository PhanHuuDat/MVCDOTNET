using Microsoft.EntityFrameworkCore;
using MVCMovie.Data;
using MVCMovie.Models;

namespace MVCMovie.Services
{
    public class MovieServices:IMovieService
    {
        private readonly MVCMovieContext _context;

        public MovieServices(MVCMovieContext context)
        {
            this._context = context;
        }
        public async Task<List<Movie>> GetMoviesAsync(string searchString, string searchGenre)
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

        public IQueryable<string> GetGenresQuery()
        {
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;
            return genreQuery;
        }
    }
}
