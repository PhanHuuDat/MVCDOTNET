using MVCMovie.Models;

namespace MVCMovie.Services
{
    public interface IMovieService
    {
        Task<List<Movie>> GetMoviesAsync(string searchString, string searchGenre);
        IQueryable<string> GetGenresQuery();
    }
}
