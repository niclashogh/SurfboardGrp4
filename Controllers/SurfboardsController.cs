﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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
            var validSurfboards = await _context.Surfboard
                .Where(surfboard => !surfboard.Rentals.Any())
                .ToListAsync();

            return View(validSurfboards);
        }

        // GET: Surfboards
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List(
            string sortOrder,
            string searchString,
            string currentFilter,
            int? pageNumber
            )
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            // ViewData["LengthSortParm"] = sortOrder == "Length" ? "length_desc" : "Length";
            ViewData["LengthSortParm"] = String.IsNullOrEmpty(sortOrder) ? "length_desc" : "";
            ViewData["TypeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var boards = from b in _context.Surfboard
            select b;

            switch (sortOrder)
            {
                case "name_desc":
                    boards = boards.OrderByDescending(s => s.Name);
                    break;
                case "length_desc":
                    boards = boards.OrderBy(s => s.Length);
                    break;
                case "type_desc":
                    boards = boards.OrderByDescending(s => s.Type);
                    break;
                default:
                    break;
            }

            int pageSize = 5;
            return View(await PaginatedList<Surfboard>.CreateAsync(boards.AsNoTracking(), pageNumber ?? 1, pageSize));
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
        public async Task<IActionResult> Create([Bind("Id,Name,Length,Width,Thickness,Volume,Type,Price,Equipment,ImgUrl,RowVersion")] Surfboard surfboard)
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
        public async Task<ActionResult> Edit(int? id, byte[] rowVersion)
        {
            string fieldsToBind = "Id,Name,Length,Width,Thickness,Volume,Type,Price,Equipment,ImgUrl,RowVersion";

            if (id == null)
            {
                return NotFound();
            }

            var departmentToUpdate = await _context.Surfboard.FindAsync(id);
            if (departmentToUpdate == null)
            {
                Surfboard deletedDepartment = new Surfboard();
                TryUpdateModelAsync(deletedDepartment, fieldsToBind);
                ModelState.AddModelError(string.Empty,
                    "Unable to save changes. The department was deleted by another user.");
                return View(deletedDepartment);
            }

            if (await TryUpdateModelAsync(departmentToUpdate, fieldsToBind))
            {
                try
                {
                    _context.Surfboard.Entry(departmentToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    _context.Update(departmentToUpdate);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Surfboard)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "Unable to save changes. The department was deleted by another user.");
                    }
                    else
                    {
                        var databaseValues = (Surfboard)databaseEntry.ToObject();

                        //if (databaseValues.Id != clientValues.Id)
                        //    ModelState.AddModelError("Id", "Current value: "
                        //        + databaseValues.Id);

                        if (databaseValues.Name != clientValues.Name)
                            ModelState.AddModelError("Name", "Current value: "
                                + String.Format("{0:c}", databaseValues.Name));

                        if (databaseValues.Length != clientValues.Length)
                            ModelState.AddModelError("Lenght", "Current value: "
                                + String.Format("{0:c}", databaseValues.Length));

                        if (databaseValues.Width != clientValues.Width)
                            ModelState.AddModelError("Width", "Current value: "
                                + String.Format("{0:c}", databaseValues.Width));

                        if (databaseValues.Thickness != clientValues.Thickness)
                            ModelState.AddModelError("Thickness", "Current value: "
                                + String.Format("{0:c}", databaseValues.Thickness));

                        if (databaseValues.Volume != clientValues.Volume)
                            ModelState.AddModelError("Volume", "Current value: "
                                + String.Format("{0:c}", databaseValues.Volume));

                        if (databaseValues.Type != clientValues.Type)
                            ModelState.AddModelError("Type", "Current value: "
                                + databaseValues.Type);

                        if (databaseValues.Price != clientValues.Price)
                            ModelState.AddModelError("Price", "Current value: "
                                + String.Format("{0:c}", databaseValues.Price));

                        if (databaseValues.ImgUrl != clientValues.ImgUrl)
                            ModelState.AddModelError("ImgUrl", "Current value: "
                                + databaseValues.ImgUrl);

                        ModelState.AddModelError(string.Empty, "The record you attempted to edit "
                            + "was modified by another user after you got the original value. The "
                            + "edit operation was canceled and the current values in the database "
                            + "have been displayed. If you still want to edit this record, click "
                            + "the Save button again. Otherwise click the Back to List hyperlink.");
                        departmentToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.)
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(departmentToUpdate);
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

            bool rentalExists = await _context.Rental.AnyAsync(rental => rental.SurfboardId == id);
            if (!rentalExists)
            {
                return View(viewModel);

            }
            return RedirectToAction("Index");
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
                // some logic to calculate price based off date period
                bool rentalExists = await _context.Rental.AnyAsync(rental => rental.SurfboardId == id);

                if (!rentalExists)
                {
                    _context.Add(viewModel.Rental);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Row", "Board already rented out");
                }

            }
            return View(viewModel);
        }
    }
}