using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTNGOL.Servicios.DTOs;
using UTNGOL.Servicios.Interface;
namespace UTNGOL.Servicios.Services;

public class MockEstadisticasService : IEstadisticasService
{
    public async Task<List<GrupoDTO>> ObtenerGruposAsync()
    {
        return await Task.FromResult(new List<GrupoDTO> {
            new GrupoDTO { IdGrupo = 1, Nombre = "Grupo A" }
        });
    }

    public async Task<List<PartidoDTO>> ObtenerPartidosAsync()
    {
        return await Task.FromResult(new List<PartidoDTO> {
            new PartidoDTO { IdPartido = 1, SeleccionLocal = "Ecuador", SeleccionVisitante = "Canadá", Estado = "Programado" }
        });
    }

    public async Task<List<PosicionDTO>> ObtenerTablaPosicionesAsync()
    {
        return await Task.FromResult(new List<PosicionDTO> {
            new PosicionDTO { Nombre = "Ecuador", Puntos = 3, PartidosJugados = 1 }
        });
    }

    public async Task<List<SeleccionDTO>> ObtenerSeleccionAsync(string codigoFifa)
    {
        return await Task.FromResult(new List<SeleccionDTO> {
            new SeleccionDTO { Nombre = "Ecuador", CodigoFifa = "ECU" }
        });
    }
}
