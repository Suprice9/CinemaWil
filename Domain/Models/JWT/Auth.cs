using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.JWT
{
    public  class Auth
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PassCode { get; set; }

        public bool Admin { get; set; } 
    }
}
