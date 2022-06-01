using MVCMovie.Data;

namespace MVCMovie.Services
{
    public class UnitOfWork
    {
        private MVCMovieContext _db;

        public UnitOfWork(MVCMovieContext db)
        {
            _db = db;
          
            GenreServices = new GenreServices(db);
        }

        
        public GenreServices GenreServices { get; private set; }

        public void Dispose()
        {
            _db.Dispose(); 
        }

        public async Task<int> SaveAsync()
        {
            
            return await _db.SaveChangesAsync();
        }
    }
}
