using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMovie.Data;
using MVCMovie.Models.ViewModels;
using MVCMovie.Models;
using MVCMovie.Services;

namespace MVCMovie.Controllers
{
    public class MoviesController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public MoviesController(UnitOfWork service)
        {
            _unitOfWork = service;
        }

        // GET: Movies
        public async Task<IActionResult> Index(string movieGenre, string searchString)
        {
            // Use LINQ to get list of genres.

            var genreQuery = _unitOfWork.MovieServices.GetGenresQuery();
            var movies = await _unitOfWork.MovieServices.GetMoviesByConditionsAsync(searchString, movieGenre);

            var movieGenreVM = new MovieGenreViewModel
            {
                Genres = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Movies = movies
            };

            return View(movieGenreVM);
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            
            if (id == null || !_unitOfWork.MovieServices.IsHasValue())
            {
                return NotFound();
            }

            var movie = await _unitOfWork.MovieServices
                .GetFirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.MovieServices.AddMovieAsync(movie);
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork.MovieServices == null)
            {
                return NotFound();
            }

            var movie = await _unitOfWork.MovieServices.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Genre,Price,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.MovieServices.Update(movie);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.MovieServices.MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _unitOfWork.MovieServices == null)
            {
                return NotFound();
            }

            var movie = await _unitOfWork.MovieServices
                .GetFirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_unitOfWork.MovieServices == null)
            {
                return Problem("Entity set 'MVCMovieContext.Movie'  is null.");
            }
            var movie = await _unitOfWork.MovieServices.FindAsync(id);
            if (movie != null)
            {
                _unitOfWork.MovieServices.Remove(movie);
            }

            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

       

    }
}
