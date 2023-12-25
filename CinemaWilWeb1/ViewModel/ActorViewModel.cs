using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CinemaWilWeb.ViewModel
{
    public class ActorViewModel
    {
        public int Id { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }
        [DisplayName("Nacionalidad")]
        public string Nationality { get; set; }
        [DisplayName("Sexo")]
        public string Gender { get; set; }
        [DisplayName("Fecha de nacimiento")]
        public DateTime Birthday { get; set; }
    }
}
