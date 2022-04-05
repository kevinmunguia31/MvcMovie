using System.Collections.Generic;
using MvcMovie.Models;

namespace MvcMovie.ViewModels
{
    public class MovieCommentsViewModel{
        public Movie Movie {get; set;}
        public List<Comments> listComments {get; set;}
    }   
}