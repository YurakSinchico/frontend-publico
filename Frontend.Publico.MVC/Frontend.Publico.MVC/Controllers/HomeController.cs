using Frontend.Publico.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Api.Consumer.Consumers;

namespace Frontend.Publico.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EstadisticasConsumer _estadisticasConsumer;

        public HomeController(
            ILogger<HomeController> logger,
            EstadisticasConsumer estadisticasConsumer)
        {
            _logger = logger;
            _estadisticasConsumer = estadisticasConsumer;
        }

        public async Task<IActionResult> Index()
        {
            // Si el usuario inició sesión
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            // Usuario invitado
            var grupos = await _estadisticasConsumer.ObtenerGruposAsync();

            return View(grupos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}