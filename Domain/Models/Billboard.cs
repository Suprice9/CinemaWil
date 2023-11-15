using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Billboard
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Movie")]
        public int MoviesId { get; set; }
        public Movie? Movie { get; set; }


        [ForeignKey("Actor")]
        public int ActorsId { get; set; }
        public Actor? Actor { get; set; }
    }
}
