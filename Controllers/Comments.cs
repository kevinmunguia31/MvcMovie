using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MvcMovie.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcMovie.Controllers
{
    public class CommentsController : Controller
    {
        public List<Comments> lstComments = null;

        public AppDbContext _context;
        public CommentsController(AppDbContext context)
        {
            /*var myJsonString = System.IO.File.ReadAllText("Models/Comments.json");
            lstComments = JsonConvert.DeserializeObject<List<Comments>>(myJsonString);*/

            _context = context;
        }

        public async Task<IActionResult> Index(int Id)
        {
            var movie = await _context.Movies.Where(m => m.Id == Id).Include(m => m.Comments).FirstAsync();
            return PartialView("~/Views/Movie/ListComments.cshtml", movie);
        }

        public async Task<IActionResult> Save(Comments model)
        {
            if (ModelState.IsValid)
            {
                model.Like = 0;
                model.Date = DateTime.Now.Date.ToShortDateString();
                _context.Comments.Add(model);
                await _context.SaveChangesAsync();
                return Redirect("/Movie/Details/" + model.MovieId);
            }
            return Redirect("/Movie/Index");
        }

        public async Task<RedirectResult> Delete(int Id)
        {
            var model = await _context.Comments.FindAsync(Id);
            _context.Comments.Remove(model);
            await _context.SaveChangesAsync();
            return Redirect("/Movie/Details/" + model.MovieId);
        }

        //public async Task<ActionResult> Like(Comments model){
          //  var movie = await _context.Comments.Update(m => model.MovieId == model.Id).FirstAsync();

            //return RedirectToAction("Index");
        //}

    }
}
