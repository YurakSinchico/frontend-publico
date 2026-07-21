using Api.Consumer.Consumers;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Publico.MVC.Controllers
{
    public class SeleccionesController : Controller
    {
        private readonly EstadisticasConsumer _estadisticasConsumer;

        public SeleccionesController(EstadisticasConsumer estadisticasConsumer)
        {
            _estadisticasConsumer = estadisticasConsumer;
        }

        // GET: Selecciones
        public async Task<IActionResult> Index()
        {
            var lista = await _estadisticasConsumer.ObtenerSeleccionesAsync();

            return View(lista);
        }

        // GET: Selecciones/Details/ECU
        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return RedirectToAction(nameof(Index));
            }

            var selecciones = await _estadisticasConsumer.ObtenerSeleccionesAsync();

            var seleccion = selecciones.FirstOrDefault(s => s.CodigoFifa == id);

            if (seleccion == null)
            {
                return NotFound();
            }

            return View(seleccion);
        }
    }
}