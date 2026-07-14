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
        Task<List<GrupoDTO>> ObtenerGruposAsync();
        Task<List<PartidoDTO>> ObtenerPartidosAsync();
        Task<List<PosicionDTO>> ObtenerTablaPosicionesAsync();
        Task<List<SeleccionDTO>> ObtenerSeleccionAsync(string codigoFifa);
    }
}

