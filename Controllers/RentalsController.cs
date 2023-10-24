using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mvc_surfboard.Data;
using mvc_surfboard.Models;

namespace mvc_surfboard.Controllers
{
    [Authorize]
    public class RentalsController : Controller
    {
        private readonly mvc_surfboardContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RentalsController(mvc_surfboardContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #region Index
        // GET: Rentals
        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Admin"))
            {
                var mvc_surfboardContext = _context.Rental
                    .Include(r => r.Surfboard)
                    .Include(r => r.User);
                return View(await mvc_surfboardContext.ToListAsync());
            }
            else
            {
                // if user isnt admin only return rentals the user has rented
                var user = await _userManager.GetUserAsync(User);
                string currentUserId = user.Id;
                var mvc_surfboardContext = _context.Rental
                    .Include(r => r.Surfboard)
                    .Include(r => r.User)
                    .Where(r => r.UserId == currentUserId);
                return View(await mvc_surfboardContext.ToListAsync());
            }
        }
        #endregion

        #region Details
        // GET: Rentals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.Surfboard)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }
        #endregion

        #region Create
        // GET: Rentals/Create
        public IActionResult Create()
        {
            ViewData["SurfboardId"] = new SelectList(_context.Surfboard, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Rentals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("RentalId,SurfboardId,StartDate,EndDate,TotalCost,RowVersion")] Rental rental)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rental);
        }
        #endregion

        #region Edit
        // GET: Rentals/Edit/5
        public async Task   <IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            ViewData["SurfboardId"] = new SelectList(_context.Surfboard, "Id", "Name", rental.SurfboardId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rental.UserId);
            return View(rental);
        }

        // POST: Rentals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("RentalId,SurfboardId,StartDate,EndDate,TotalCost,RowVersion")] Rental rental)
        {
            if (id != rental.RentalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rental);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RentalExists(rental.RentalId))
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
            ViewData["SurfboardId"] = new SelectList(_context.Surfboard, "Id", "Name", rental.SurfboardId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", rental.UserId);
            return View(rental);
        }
        #endregion

        #region Delete
        // GET: Rentals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rental == null)
            {
                return NotFound();
            }

            var rental = await _context.Rental
                .Include(r => r.Surfboard)
                .Include(r => r.User)
                .FirstOrDefaultAsync(m => m.RentalId == id);
            if (rental == null)
            {
                return NotFound();
            }

            return View(rental);
        }

        // POST: Rentals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rental == null)
            {
                return Problem("Entity set 'mvc_surfboardContext.Rental'  is null.");
            }
            var rental = await _context.Rental.FindAsync(id);
            if (rental != null)
            {
                _context.Rental.Remove(rental);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        private bool RentalExists(int id)
        {
            return (_context.Rental?.Any(e => e.RentalId == id)).GetValueOrDefault();
        }
    }
}
