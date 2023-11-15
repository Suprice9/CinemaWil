﻿using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos
{
    public class ActorDto
    { 
       public string Name { get; set; }

       public string Nationality { get; set; }

       public string Gender { get; set; }

       public DateTime Birthday { get; set; }
       
    }
}
