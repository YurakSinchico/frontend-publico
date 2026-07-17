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
        public async Task<IActionResult> Index()
        {
            // Llamamos al método de la interfaz que definimos antes
            var posiciones = await _service.ObtenerTablaPosicionesAsync();

            // Enviamos la lista a la vista que debe tener @model List<PosicionDTO>
            return View(posiciones);
        }

    }
}
