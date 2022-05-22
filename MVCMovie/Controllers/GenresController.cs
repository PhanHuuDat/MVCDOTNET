using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCMovie.Data;
using MVCMovie.Models;
using MVCMovie.Services;

namespace MVCMovie.Controllers
{
    public class GenresController : Controller
    {
        private readonly UnitOfWork _unitOfWork;

        public GenresController(UnitOfWork context)
        {
            _unitOfWork = context;
        }

        // GET: Genres
        public async Task<IActionResult> Index()
        {
              return _unitOfWork.GenreServices != null ? 
                          View(await _unitOfWork.GenreServices.ToListAsync()) :
                          Problem("Entity set 'MVCMovieContext.Genre'  is null.");
        }

        // GET: Genres/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork.GenreServices == null)
            {
                return NotFound();
            }

            var genre = await _unitOfWork.GenreServices
                .GetFirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // GET: Genres/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CreatedDate")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.GenreServices.AddGenreAsync(genre);
                return RedirectToAction(nameof(Index));
            }
            return View(genre);
        }

        // GET: Genres/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _unitOfWork.GenreServices == null)
            {
                return NotFound();
            }

            var genre = await _unitOfWork.GenreServices.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }
            return View(genre);
        }

        // POST: Genres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CreatedDate")] Genre genre)
        {
            if (id != genre.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _unitOfWork.GenreServices.Update(genre);
                    await _unitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.GenreServices.GenreExists(genre.Id))
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
            return View(genre);
        }

        // GET: Genres/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _unitOfWork.GenreServices == null)
            {
                return NotFound();
            }

            var genre = await _unitOfWork.GenreServices
                .GetFirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return View(genre);
        }

        // POST: Genres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_unitOfWork.GenreServices == null)
            {
                return Problem("Entity set 'MVCMovieContext.Genre'  is null.");
            }
            var genre = await _unitOfWork.GenreServices.FindAsync(id);
            if (genre != null)
            {
                _unitOfWork.GenreServices.Remove(genre);
            }
            
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        
    }
}
