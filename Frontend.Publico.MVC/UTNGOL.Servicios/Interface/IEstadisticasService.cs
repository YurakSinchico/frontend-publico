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
        // Dentro de IEstadisticasService.cs
        Task<GrupoDTO> ObtenerTablaPosicionesAsync(int idGrupo);
        Task<List<GrupoDTO>> ObtenerGruposAsync();

        // Y este para los grupos
        Task<List<PartidoDTO>> ObtenerPartidosAsync();

        // Y este para selecciones
        Task<List<SeleccionDTO>> ObtenerSeleccionesAsync();
    }
}

