using Microsoft.EntityFrameworkCore;
using MVCMovie.Data;
using MVCMovie.Models;
using System.Linq.Expressions;

namespace MVCMovie.Services
{
    public class GenreServices
    {
        private readonly MVCMovieContext _db;

        public GenreServices(MVCMovieContext db)
        {
            _db = db;
        }

        public List<Genre> ToList()
        {
            return _db.Genre.ToList();
        }

        public async Task<List<Genre>> ToListAsync()
        {
            return await _db.Genre.ToListAsync();
        }

        public async Task<Genre> GetFirstOrDefaultAsync(Expression<Func<Genre, bool>>? filter = null)
        {
            IQueryable<Genre>? query = _db.Genre;
            if (filter != null)
            {
                query = query?.Where(filter);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task AddGenreAsync(Genre genre)
        {
            await _db.AddAsync(genre);
            await _db.SaveChangesAsync();
        }

        public async Task<Genre> FindAsync(params object?[]? keyValue)
        {
            return await _db.Genre.FindAsync(keyValue);
        }

        public void Update(Genre genre)
        {
            _db.Update(genre);
        }
        public void Remove(Genre genre)
        {
            _db.Remove(genre);
        }

        public bool GenreExists(int id)
        {
            return (_db.Genre?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
