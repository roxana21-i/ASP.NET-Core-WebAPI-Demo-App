using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CititesManager.WebAPI.DatabaseContext;
using CititesManager.WebAPI.Models;

namespace CititesManager.WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CitiesController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        /// <summary>
        /// Get the list of cities (city ID and city name) currently in the 'Cities' table
        /// </summary>
        /// <returns>The list of cities</returns>
        [HttpGet]
        //[Produces("application/xml")]
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            if (_context.Cities == null)
            {
                return NotFound();
            }

            return await _context.Cities.OrderBy(temp => temp.CityName).ToListAsync();
        }

        // GET: api/Cities/5
        /// <summary>
        /// Get a single city value based on the given ID
        /// </summary>
        /// <param name="id">The CityID to match</param>
        /// <returns>The matching city if succesful, error if not</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(Guid id)
        {
            if (_context.Cities == null)
            {
                return NotFound();
            }
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return Problem(detail: "Invalid CityID", statusCode: 400, title: "City Search");
                //return NotFound();
            }

            return city; //ActionResult<T> for when you will return a model object -> ObjectResult
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Update data for a specific city
        /// </summary>
        /// <param name="id">CityID to match</param>
        /// <param name="city">Updated name of matching city</param>
        /// <returns>Empty result is succesful, error is not</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(Guid id,
            [Bind(nameof(City.CityID), nameof(City.CityName))] City city)
        {
            if (id != city.CityID)
            {
                return BadRequest();
            }

            //_context.Entry(city).State = EntityState.Modified; //modifies all values
            City existingCity = await _context.Cities.FindAsync(id);

            if (existingCity == null)
            {
                return NotFound();
            }

            existingCity.CityName = city.CityName;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Insert a new city into the databse
        /// </summary>
        /// <param name="city">City to insert</param>
        /// <returns>Inserted city if succesful, error if not</returns>
        [HttpPost]
        public async Task<ActionResult<City>> PostCity(
            [Bind(nameof(City.CityID), nameof(City.CityName))] City city)
        {
            if (_context.Cities == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cities'  is null.");
            }
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { id = city.CityID }, city);
        }

        // DELETE: api/Cities/5
        /// <summary>
        /// Delete a city from the database
        /// </summary>
        /// <param name="id">CityID to match</param>
        /// <returns>Empty result if succesful, error if not</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            if (_context.Cities == null)
            {
                return NotFound();
            }
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(Guid id)
        {
            return (_context.Cities?.Any(e => e.CityID == id)).GetValueOrDefault();
        }
    }
}
