using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CititesManager.WebAPI.DatabaseContext;
using CititesManager.WebAPI.Models;

namespace CititesManager.WebAPI.Controllers.v2
{
    [ApiVersion("2.0")]
    public class CitiesController : CustomControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cities
        /// <summary>
        /// Get the list of cities (city name) currently in the 'Cities' table
        /// </summary>
        /// <returns>The list of cities (city name only)</returns>
        [HttpGet]
        //[Produces("application/xml")]
        public async Task<ActionResult<IEnumerable<string?>>> GetCities()
        {
            if (_context.Cities == null)
            {
                return NotFound();
            }

            var cities = await _context.Cities.OrderBy(temp => temp.CityName).Select(temp => temp.CityName)
                .ToListAsync();

            return cities;
        }
    }
}
