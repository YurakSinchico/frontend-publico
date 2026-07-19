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
    
}
}