using MVCMovie.Data;

namespace MVCMovie.Services
{
    public class GenreServices
    {
        private readonly MVCMovieContext _db;

        public GenreServices(MVCMovieContext db)
        {
            _db = db;
        }

    }
}
