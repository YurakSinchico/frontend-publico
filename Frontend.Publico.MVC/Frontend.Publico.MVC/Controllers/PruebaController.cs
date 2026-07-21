using Api.Consumer.Consumers;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Publico.MVC.Controllers
{
    public class PruebaController : Controller
    {
        private readonly EstadisticasConsumer _estadisticasConsumer;

        public PruebaController(EstadisticasConsumer estadisticasConsumer)
        {
            _estadisticasConsumer = estadisticasConsumer;
        }

        // GET: Prueba
        public async Task<IActionResult> Index()
        {
            var grupos = await _estadisticasConsumer.ObtenerGruposAsync();
            return View(grupos);
        }
    }
}