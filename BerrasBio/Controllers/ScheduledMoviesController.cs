#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BerrasBio.Data;
using BerrasBio.Models;

namespace BerrasBio.Controllers
{
    public class ScheduledMoviesController : Controller
    {
        private readonly DataContext _context;

        public ScheduledMoviesController(DataContext context)
        {
            _context = context;
        }

        // GET: ScheduledMovies
        public async Task<IActionResult> Index()
        {
            var dataContext = _context.ScheduledMovies.Include(s => s.Auditorium).Include(s => s.Movie);
            return View(await dataContext.ToListAsync());
        }

        // GET: ScheduledMovies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduledMovie = await _context.ScheduledMovies
                .Include(s => s.Auditorium)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduledMovie == null)
            {
                return NotFound();
            }

            return View(scheduledMovie);
        }

        // GET: ScheduledMovies/Create
        public IActionResult Create()
        {
            ViewData["AuditoriumId"] = new SelectList(_context.CinemaAuditoriums, "Id", "Id");
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
            return View();
        }

        // POST: ScheduledMovies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MovieId,AuditoriumId,Start,End,SeatsLeft")] ScheduledMovie scheduledMovie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduledMovie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuditoriumId"] = new SelectList(_context.CinemaAuditoriums, "Id", "Id", scheduledMovie.AuditoriumId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", scheduledMovie.MovieId);
            return View(scheduledMovie);
        }

        // GET: ScheduledMovies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduledMovie = await _context.ScheduledMovies.FindAsync(id);
            if (scheduledMovie == null)
            {
                return NotFound();
            }
            ViewData["AuditoriumId"] = new SelectList(_context.CinemaAuditoriums, "Id", "Id", scheduledMovie.AuditoriumId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", scheduledMovie.MovieId);
            return View(scheduledMovie);
        }

        // POST: ScheduledMovies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MovieId,AuditoriumId,Start,End,SeatsLeft")] ScheduledMovie scheduledMovie)
        {
            if (id != scheduledMovie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduledMovie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduledMovieExists(scheduledMovie.Id))
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
            ViewData["AuditoriumId"] = new SelectList(_context.CinemaAuditoriums, "Id", "Id", scheduledMovie.AuditoriumId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", scheduledMovie.MovieId);
            return View(scheduledMovie);
        }

        // GET: ScheduledMovies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduledMovie = await _context.ScheduledMovies
                .Include(s => s.Auditorium)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (scheduledMovie == null)
            {
                return NotFound();
            }

            return View(scheduledMovie);
        }

        // POST: ScheduledMovies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheduledMovie = await _context.ScheduledMovies.FindAsync(id);
            _context.ScheduledMovies.Remove(scheduledMovie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduledMovieExists(int id)
        {
            return _context.ScheduledMovies.Any(e => e.Id == id);
        }
    }
}
