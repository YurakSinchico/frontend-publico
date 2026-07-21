using Api.Consumer.Consumers;
using Microsoft.AspNetCore.Mvc;
using UTNGOL.Models;

namespace Frontend.Publico.MVC.Controllers
{
    public class PosicionesController : Controller
    {
        private readonly EstadisticasConsumer _estadisticasConsumer;

        public PosicionesController(EstadisticasConsumer estadisticasConsumer)
        {
            _estadisticasConsumer = estadisticasConsumer;
        }

        // GET: Posiciones
        public async Task<IActionResult> Index(int id = 1)
        {
            var grupo = await _estadisticasConsumer.ObtenerTablaPosicionesAsync(id);

            if (grupo?.Posiciones == null)
            {
                return View(new List<PosicionDTO>());
            }

            return View(grupo.Posiciones);
        }
    }
}