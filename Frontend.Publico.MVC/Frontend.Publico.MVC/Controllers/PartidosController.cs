using Api.Consumer.Consumers;
using Microsoft.AspNetCore.Mvc;
using UTNGOL.Models;

namespace Frontend.Publico.MVC.Controllers
{
    public class PartidosController : Controller
    {
        private readonly EstadisticasConsumer _estadisticasConsumer;

        public PartidosController(EstadisticasConsumer estadisticasConsumer)
        {
            _estadisticasConsumer = estadisticasConsumer;
        }

        // GET: Partidos
        public async Task<IActionResult> Index()
        {
            var partidos = await _estadisticasConsumer.ObtenerPartidosAsync();
            return View(partidos);
        }

        // GET: Partidos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var partido = await _estadisticasConsumer.ObtenerPartidoPorIdAsync(id);

            if (partido == null)
                return NotFound();

            return View(partido);
        }
    }
}