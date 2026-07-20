using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTNGOL.Servicios.DTOs
{
    public class UserInputDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? IdRole { get; set; }
        public bool? Active { get; set; }
    }
}
