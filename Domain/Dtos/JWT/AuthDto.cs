using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos.JWT
{
    public class AuthDto
    {
        public string Name { get; set; }

        public string PassCode { get; set; }
    }
}
