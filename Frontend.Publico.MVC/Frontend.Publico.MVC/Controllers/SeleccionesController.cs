using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UTNGOL.Servicios.Interface;

namespace Frontend.Publico.MVC.Controllers
{
    public class SeleccionesController : Controller
    {
        private readonly IEstadisticasService _estadisticasService;

        public SeleccionesController(IEstadisticasService estadisticasService)
        {
            _estadisticasService = estadisticasService;
        }

        // Al entrar a /Selecciones o /Selecciones/Index, verás la lista
        public async Task<IActionResult> Index()
        {
            // Traemos todas las selecciones de una vez
            var lista = await _estadisticasService.ObtenerSeleccionesAsync();

            // Enviamos la lista entera a la vista
            return View(lista);
        }
        [Route("Selecciones/Details/{id?}")] // El signo '?' hace que el ID sea opcional
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index"); // Si no hay ID, te manda al listado en vez de dar error
            }

            var todasLasSelecciones = await _estadisticasService.ObtenerSeleccionesAsync();
            var seleccion = todasLasSelecciones?.FirstOrDefault(s => s.CodigoFifa == id);

            if (seleccion == null)
            {
                return NotFound();
            }

            return View(seleccion);
        }

    }
}