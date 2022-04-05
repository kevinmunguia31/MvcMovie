using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Comments{
        public int Id { get; set; }
        public string Date { get; set; }
        public string Text { get; set; }
        public int Like {get; set;}
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

    }
}