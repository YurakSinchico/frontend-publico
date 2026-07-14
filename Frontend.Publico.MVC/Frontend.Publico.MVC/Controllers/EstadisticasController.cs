using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UTNGOL.Servicios.Interface;

namespace Frontend.Publico.MVC.Controllers
{
    public class EstadisticasController : Controller
    {
        private readonly IEstadisticasService _estadisticasService;
        public EstadisticasController(IEstadisticasService estadisticasService)
        {
            _estadisticasService = estadisticasService;
        }
        // GET: EstadisticasController
        public async Task<IActionResult> Index()
        {
            // Aquí obtienes la data de tu servicio
            var posiciones = await _estadisticasService.ObtenerTablaPosicionesAsync();
            return View(posiciones);
        }

        // GET: EstadisticasController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EstadisticasController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadisticasController/Create
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

        // GET: EstadisticasController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EstadisticasController/Edit/5
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

        // GET: EstadisticasController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EstadisticasController/Delete/5
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
