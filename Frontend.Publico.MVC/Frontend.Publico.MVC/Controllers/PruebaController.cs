using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using UTNGOL.Servicios.Interface; // Asegúrate de tener este using

namespace Frontend.Publico.MVC.Controllers
{
    public class PruebaController : Controller
    {
        private readonly IEstadisticasService _estadisticasService;

        // Inyectamos el servicio real a través del constructor
        public PruebaController(IEstadisticasService estadisticasService)
        {
            _estadisticasService = estadisticasService;
        }

        // GET: PruebaController
        public async Task<IActionResult> Index()
        {
            // Consumimos el API real desde el backend en Fedora
            var grupos = await _estadisticasService.ObtenerGruposAsync();

            // Enviamos los datos reales a la vista
            return View(grupos);
        }
    }
}