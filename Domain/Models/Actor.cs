﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Nationality { get; set; }

        public string Gender { get; set; }

        public DateTime Birthday { get; set; }

     
    }
}
