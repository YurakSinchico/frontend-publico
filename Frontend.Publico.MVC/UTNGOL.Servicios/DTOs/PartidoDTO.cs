using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTNGOL.Servicios.DTOs
{
    public class PartidoDTO
    {
        public int IdPartido { get; set; }
        public int NumeroPartidoFifa { get; set; }
        public DateTime FechaHoraUtc { get; set; }
        public string Estado { get; set; }
        public string SeleccionLocal { get; set; }
        public string SeleccionVisitante { get; set; }
        public int? GolesLocal { get; set; }
        public int? GolesVisitante { get; set; }
        public string Estadio { get; set; }
        public string Grupo { get; set; }
        public string Fase { get; set; }
    }
}
