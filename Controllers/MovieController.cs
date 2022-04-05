using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MvcMovie.Models;
using MvcMovie.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace MvcMovie.Controllers
{
    public class MovieController : Controller
    {
        private AppDbContext _context;

        public List<Movie> lstMovies = null;

        public List<Comments> lstComments = null;
        public MovieController(AppDbContext context)
        {
            _context = context;
            /*var myJsonString = System.IO.File.ReadAllText("Models/Movie.json");
            lstMovies = JsonConvert.DeserializeObject<List<Movie>>(myJsonString);*/
             
           /* var myJsonString = System.IO.File.ReadAllText("Models/Comments.json");
            lstComments = JsonConvert.DeserializeObject<List<Comments>>(myJsonString);*/
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _context.Movies.ToListAsync();

            /*foreach(var movie in movies){
                movie.Image = JsonConvert.DeserializeObject<List<string>>(movie.ImagesJson);
            }*/


            return View(movies);
        }

        [Authorize]
        [Authorize(Roles = "Admin, Editor")]
        public IActionResult Add()
        {
            return View(new Movie());
        }
        public async Task<IActionResult> Save(Movie model)
        {

            if(ModelState.IsValid){
                _context.Movies.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", await _context.Movies.ToListAsync());
            }
            return View("Add", model);
        }

        public async Task<IActionResult> Details(int Id){
            var movie = await _context.Movies.Where(m => m.Id == Id).Include(m => m.Comments).FirstAsync();
           /* movie.Image = JsonConvert.DeserializeObject<List<string>>(movie.ImagesJson);*/
            
            /*ViewData["lstComments"] = lstComments;*/

            /*MovieCommentsViewModel mcvm = new MovieCommentsViewModel();
            mcvm.Movie = movie;
            mcvm.listComments = lstComments;*/

            return View(movie);
        }

        public async Task<IActionResult> Find(string movie2){

            var movie = await _context.Movies.ToListAsync();
            List<Movie> listResultMovie = new List<Movie>();

            foreach (var item in movie)
            {
                if (item.Title.ToLower().Contains(movie2.ToLower()))
                {
                    listResultMovie.Add(item);
                }
            }

            return View("Index", listResultMovie);
        }

        [Authorize(Roles = "Admin")]
        public async Task<RedirectResult> Delete(int Id)
        {
            var movie = await _context.Movies.FindAsync(Id);
            
           // if(movie == null)
             //   return NotFound();

            _context.Remove(movie);
            await _context.SaveChangesAsync();
            return Redirect("https://localhost:5001/Movie");
        }

        public async Task<ActionResult> Edit(int Id){
            var movie = await _context.Movies.FindAsync(Id);
            return View(movie);
        }

        [HttpPost]
        public async Task<ActionResult> Update(Movie model){
             if(ModelState.IsValid){
                _context.Movies.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", await _context.Movies.ToListAsync());
            }
            return View("Edit", model);
        }

    }
}