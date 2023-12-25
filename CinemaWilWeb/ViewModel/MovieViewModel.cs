using Domain.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CinemaWilWeb.ViewModel
{
    public class MovieViewModel
    {
        
        public int Id { get; set; }
        [DisplayName("Titulo")]
        public string Title { get; set; }
        [DisplayName("Genero")]
        public string Genre { get; set; }
    }
}
