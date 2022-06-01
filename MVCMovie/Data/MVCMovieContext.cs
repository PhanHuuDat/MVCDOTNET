using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCMovie.Models;

namespace MVCMovie.Data
{
    public class MVCMovieContext : DbContext
    {
        public MVCMovieContext (DbContextOptions<MVCMovieContext> options)
            : base(options)
        {
        }

        public DbSet<Movie>? Movie { get; set; }

        public DbSet<Genre>? Genre { get; set; }

        public DbSet<MVCMovie.Models.Company> Company { get; set; }

        public DbSet<MVCMovie.Models.Person> Person { get; set; }
    }
}
