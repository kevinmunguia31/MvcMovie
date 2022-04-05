using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MvcMovie.Models;
using MvcMovie.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Controllers
{
    [Route("apimovie")]
    [ApiController]
    public class ApiMovieController : Controller{
        private AppDbContext _context;

        public ApiMovieController(AppDbContext context){
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetApiMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            return movies;
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetApiMovie(int Id)
        {
            var movie = await _context.Movies.Where(m => m.Id == Id).Include(m => m.Comments).FirstAsync();
            return movie;
        }

        [HttpGet("findtitle/{title}")]
        public async Task<ActionResult<List<Movie>>> ApiMoviesFindTitle(string title)
        {
            var movies = await _context.Movies.Where(
                m => EF.Functions.Like(m.Title, "%" + title +"%")).Include(m => m.Comments)
                .ToListAsync();
            return movies;
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostApiMovie(Movie model)
        {
            if (ModelState.IsValid)
            {
                _context.Movies.Add(model);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetApiMovie), new { id = model.Id });
            }

            return NotFound();
            
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteApiMovie(int Id)
        {
            var movie = await _context.Movies.FindAsync(Id);
            
            if(movie == null)
                return NotFound();

            _context.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}