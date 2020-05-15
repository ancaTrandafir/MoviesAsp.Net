using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Movies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Movies;
using RestSharp.Extensions;

namespace HotelMng.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {

        private readonly MoviesContext _context;


        public MoviesController(MoviesContext context)
        {
            _context = context;

        }



        // GET: movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetReservations()
        {

            return await _context.Movies.ToListAsync();
        }




        // GET: movie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(long id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }
            return movie;
        }




        // GET: movies/filter?from=a&to=b
        [HttpGet("filter")]
        public IOrderedQueryable<Movie> GetFilteredMovies(String from, String to )
        {
            DateTime fromDate =  Convert.ToDateTime(from);
            DateTime toDate = Convert.ToDateTime(to);
            //  DateTime toDate = DateTime.ParseExact(to, "dd.MM.yyyy", null);

            // LINQ
            var results = _context.Movies.Where(o => fromDate.CompareTo(o.DateAdded) == -1 && toDate.CompareTo(o.DateAdded) == -1);

            var sortedResultsByYearOfRelease = results.OrderBy(o => o.YearOfRelease);  

            return sortedResultsByYearOfRelease;  // type IOrderedQueryable
        }






        // PUT: movie/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(long id, Movie movie)
        {
            if (id != movie.ID)
            {
                return BadRequest();
            }

            if (!MovieExists(id))
            {
                return NotFound();
            }

            _context.Entry(movie).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(movie);
        }






        // POST: /movie
        [HttpPost]
        public async Task<IActionResult> PostMovie(Movie movie)
        {

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();


             return CreatedAtAction("GetMovie", new { id = movie.ID }, movie);
          //  return Ok(movie);
        }






        // DELETE: /5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> DeleteMovie(long id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            _context.Entry(movie).State = EntityState.Deleted;
            await _context.SaveChangesAsync();

            return movie;
        }



        private bool MovieExists(long id)
        {
            return _context.Movies.Any(e => e.ID == id);
        }






    






    }
}

