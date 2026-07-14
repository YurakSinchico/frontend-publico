using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTNGOL.Servicios.DTOs
{
    public class GrupoDTO
    {
        public int IdGrupo { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public List<PosicionDTO> Posiciones { get; set; }
    }
}
