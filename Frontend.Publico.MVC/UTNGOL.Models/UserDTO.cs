using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UTNGOL.Models
{
    public class UserDTO
    {
        public int IdUser { get; set; }

        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Username { get; set; }

        public bool Active { get; set; }

        // Java envía timestamps en milisegundos
        public long? RegisteredAt { get; set; }

        public long? LastAccess { get; set; }

        public string? Role { get; set; }

        public int? IdRole { get; set; }
    }
}