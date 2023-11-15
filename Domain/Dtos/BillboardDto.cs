using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class BillboardDto
    {
        public int MoviesId { get; set; }

        public int ActorsId { get; set; }

    }
}
