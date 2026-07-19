using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTNGOL.Servicios.Interface;

namespace Frontend.Publico.MVC.Controllers
{
    public class PosicionesController : Controller
    {
        private readonly IEstadisticasService _service;
        public PosicionesController(IEstadisticasService service)
        {
            _service = service;
        }
        // GET: PosicionesController
        public async Task<IActionResult> Index(int id = 1)
        {
            // 1. Llamas al servicio para traer el grupo completo
            var grupo = await _service.ObtenerTablaPosicionesAsync(id);

            // 2. Si el grupo existe, envías solo la lista a la vista
            if (grupo != null && grupo.Posiciones != null)
            {
                return View(grupo.Posiciones);
            }

            // 3. Si no hay datos, envías una lista vacía para que no se caiga
            return View(new List<UTNGOL.Servicios.DTOs.PosicionDTO>());
        }

    }
}
