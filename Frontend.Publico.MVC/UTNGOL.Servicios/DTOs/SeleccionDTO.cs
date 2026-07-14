using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTNGOL.Servicios.DTOs
{
    public class SeleccionDTO
    {
        public int IdSeleccion { get; set; }
        public string Nombre { get; set; }
        public string CodigoFifa { get; set; }
        public bool EsAnfitrion { get; set; }
        public string Clasificacion { get; set; }
        public string Grupo { get; set; }
        public string Confederacion { get; set; }
        public int PartidosJugados { get; set; }
        public int Puntos { get; set; }
        public int Victorias { get; set; }
        public int Empates { get; set; }
        public int Derrotas { get; set; }
        public int GolesFavor { get; set; }
        public int GolesContra { get; set; }
        public int DiferenciaGoles { get; set; }
    }
}
