using Frontend.Publico.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UTNGOL.Servicios.Interface; // Asegúrate de tener este using

namespace Frontend.Publico.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEstadisticasService _service; // 1. Variable para el servicio

        // 2. Inyectamos el servicio en el constructor
        public HomeController(ILogger<HomeController> logger, IEstadisticasService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            // 3. Llamamos a la API para obtener los datos
            var grupos = await _service.ObtenerGruposAsync();

            // Enviamos los datos a la vista
            return View(grupos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}