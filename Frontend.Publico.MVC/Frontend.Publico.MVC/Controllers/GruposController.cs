using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTNGOL.Servicios.Interface;

namespace Frontend.Publico.MVC.Controllers
{
    public class GruposController : Controller
    {
        private readonly IEstadisticasService _estadisticasService;

        public GruposController(IEstadisticasService estadisticasService)
        {
            _estadisticasService = estadisticasService;
        }
        // GET: GruposController
        // GET: GruposController
        public async Task<IActionResult> Index()
        {
            // 1. Obtener todas las selecciones de la API usando el nombre correcto: _estadisticasService
            var selecciones = await _estadisticasService.ObtenerSeleccionesAsync();

            // 2. Agruparlas por la propiedad Grupo
            // Asegúrate que en tu SeleccionDTO la propiedad se llame exactamente 'Grupo'
            var gruposAgrupados = selecciones.GroupBy(s => s.Grupo)
                                            .OrderBy(g => g.Key);

            return View(gruposAgrupados);
        }

        // GET: GruposController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: GruposController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GruposController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GruposController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: GruposController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GruposController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: GruposController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
