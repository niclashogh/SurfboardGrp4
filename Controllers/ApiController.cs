using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_surfboard.Data;
using mvc_surfboard.Models;
using System.Text.Json;

namespace mvc_surfboard.Controllers
{
    [Route("api/surfboards")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly mvc_surfboardContext _context;

        public ApiController(mvc_surfboardContext context)
        {
            _context = context;
        }

        // GET: api/Surfboards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Surfboard>>> GetSurfboard()
        {
          if (_context.Surfboard == null)
          {
              return NotFound();
          }

            var validSurfboards = await _context.Surfboard
                  .Where(surfboard => !surfboard.Rentals.Any())
                  .ToListAsync();

           
            var jsonSerialized = JsonSerializer.Serialize(validSurfboards);

            return Ok(jsonSerialized);
        }

        // POST: api/Rentals
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}/rent")]
        public async Task<ActionResult<Rental>> PostRental(int id, [FromBody] Rental rental)
        {
            if (_context.Rental == null)
            {
                return Problem("Entity set 'mvc_surfboardContext.Rental' is null.");
            }

            rental.SurfboardId = id;

            _context.Rental.Add(rental);
            await _context.SaveChangesAsync();

            bool rentalExists = await _context.Rental.AnyAsync(rental => rental.SurfboardId == id);
            if (!rentalExists)
            {

                _context.Rental.Add(rental);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetRental", new { id = rental.RentalId }, rental);
            } else
            {
                return Problem("Surfboard has already been rented.");
            }

        }


        //// POST: api/Surfboards1
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Surfboard>> RentSurfboard(Rental rental)
        //{
        //    if (_context.Surfboard == null)
        //    {
        //        return Problem("Entity set 'mvc_surfboardContext.Surfboard'  is null.");
        //    }
        //    _context.Surfboard.Add(rental);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSurfboard", new { id = surfboard.Id }, surfboard);
        //}



        #region
        //// GET: api/Surfboards1/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Surfboard>> GetSurfboard(int id)
        //{
        //  if (_context.Surfboard == null)
        //  {
        //      return NotFound();
        //  }
        //    var surfboard = await _context.Surfboard.FindAsync(id);

        //    if (surfboard == null)
        //    {
        //        return NotFound();
        //    }

        //    return surfboard;
        //}

        //// PUT: api/Surfboards1/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSurfboard(int id, Surfboard surfboard)
        //{
        //    if (id != surfboard.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(surfboard).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!SurfboardExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Surfboards1
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Surfboard>> PostSurfboard(Surfboard surfboard)
        //{
        //  if (_context.Surfboard == null)
        //  {
        //      return Problem("Entity set 'mvc_surfboardContext.Surfboard'  is null.");
        //  }
        //    _context.Surfboard.Add(surfboard);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetSurfboard", new { id = surfboard.Id }, surfboard);
        //}

        //// DELETE: api/Surfboards1/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteSurfboard(int id)
        //{
        //    if (_context.Surfboard == null)
        //    {
        //        return NotFound();
        //    }
        //    var surfboard = await _context.Surfboard.FindAsync(id);
        //    if (surfboard == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Surfboard.Remove(surfboard);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool SurfboardExists(int id)
        //{
        //    return (_context.Surfboard?.Any(e => e.Id == id)).GetValueOrDefault();
        //}
        #endregion
    }
}
