using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace MvcMovie.Models
{
    public class Movie{
        public int Id {get; set;}
        
        [Required]
        [Display(Name = "Nombre")]
        public string Title {get; set;}

        [Display(Name = "Restricciones")]
        public string Runtime {get; set;}

        [Required]
        [Display(Name = "Precio")]
        public string Released {get; set;}

        [Required]
        [Display(Name = "Descripción")]
        public string Plot {get; set;}

        [Display(Name = "Presentacion")]
        public string Genre {get; set;}

        [Display(Name = "Imagen del Farmaco")]
        public string Poster {get; set;}

        public List<Comments> Comments {get; set;}

        public Movie(){
            Comments = new List<Comments>();
        }

        /*[NotMapped]        
        public List<string> Image {get; set;}

        [Required]
        [Display(Name = "Imagene en Json")]
        [Column(TypeName = "json")]
        public string ImagesJson {get; set;}*/
    }
}