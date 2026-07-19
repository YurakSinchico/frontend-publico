using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTNGOL.Servicios.DTOs;
namespace UTNGOL.Servicios.Interface
{
    public interface IEstadisticasService
    {
        Task<List<PartidoDTO>> ObtenerPartidosAsync();
        Task<List<PosicionDTO>> ObtenerTablaPosicionesAsync();
        // Y este para los grupos
        Task<List<GrupoDTO>> ObtenerGruposAsync();

        // Y este para selecciones
        Task<List<SeleccionDTO>> ObtenerSeleccionesAsync();
    }
}

