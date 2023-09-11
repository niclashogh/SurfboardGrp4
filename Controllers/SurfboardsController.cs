using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_surfboard.Data;
using mvc_surfboard.Models;

namespace mvc_surfboard.Controllers
{
    public class SurfboardsController : Controller
    {
        private readonly mvc_surfboardContext _context;

        public SurfboardsController(mvc_surfboardContext context)
        {
            _context = context;
        }

        // GET: Surfboards
        public async Task<IActionResult> Index()
        {
            return _context.Surfboard != null ?
                        View("Catalog", await _context.Surfboard.ToListAsync()) :
                        Problem("Entity set 'mvc_surfboardContext.Surfboard'  is null.");
        }

        // GET: Surfboards
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Table()
        {
            return _context.Surfboard != null ?
                        View("Table", await _context.Surfboard.ToListAsync()) :
                        Problem("Entity set 'mvc_surfboardContext.Surfboard'  is null.");
        }

        [Authorize(Roles = "Admin")]
        // GET: Surfboards/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Surfboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("Id,Name,Length,Width,Thickness,Volume,Type,Price,Equipment,ImgUrl")] Surfboard surfboard)
        {
            if (ModelState.IsValid)
            {
                _context.Add(surfboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(surfboard);
        }

        // GET: Surfboards/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Surfboard == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboard.FindAsync(id);
            if (surfboard == null)
            {
                return NotFound();
            }
            return View(surfboard);
        }

        // POST: Surfboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Length,Width,Thickness,Volume,Type,Price,Equipment,ImgUrl")] Surfboard surfboard)
        {
            if (id != surfboard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(surfboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SurfboardExists(surfboard.Id))
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
            return View(surfboard);
        }

        // GET: Surfboards/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Surfboard == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surfboard == null)
            {
                return NotFound();
            }

            return View(surfboard);
        }

        // POST: Surfboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Surfboard == null)
            {
                return Problem("Entity set 'mvc_surfboardContext.Surfboard'  is null.");
            }
            var surfboard = await _context.Surfboard.FindAsync(id);
            if (surfboard != null)
            {
                _context.Surfboard.Remove(surfboard);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SurfboardExists(int id)
        {
            return (_context.Surfboard?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // GET: Surfboards/Rent/5
        public async Task<IActionResult> Rent(int? id)
        {
            if (id == null || _context.Surfboard == null)
            {
                return NotFound();
            }

            var surfboard = await _context.Surfboard
                .FirstOrDefaultAsync(m => m.Id == id);
            if (surfboard == null)
            {
                return NotFound();
            }

            var rental = new Rental(); // You need to populate this with appropriate data

            var viewModel = new SurfboardRentalViewModel
            {
                Surfboard = surfboard,
                Rental = rental
            };

            return View(viewModel);
        }


        // POST: Surfboards/Rent
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Rent(int id, SurfboardRentalViewModel viewModel)
        {

            if (ModelState.IsValid)
            {
                viewModel.Rental.UserId = "fabb9505-7014-4305-9d88-6988b4774ad2";
                viewModel.Rental.SurfboardId = id;
                viewModel.Rental.TotalCost = 99;

                _context.Add(viewModel.Rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           var errors = ModelState.Values.SelectMany(v => v.Errors); // see errors

            return View(viewModel);
        }
    }
}
