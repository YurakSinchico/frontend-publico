using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using UTNGOL.Servicios.Interface;

namespace Frontend.Publico.MVC.Controllers
{
    public class CalendarioController : Controller
    {

        private readonly IEstadisticasService _service;

        public CalendarioController(IEstadisticasService service)
        {
            _service = service;
        }

        // GET: CalendarioController
        public async Task<IActionResult> Index()
        {
            var partidos = await _service.ObtenerPartidosAsync();
            return View(partidos); // Envía los datos a la vista
        }

        // GET: CalendarioController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CalendarioController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CalendarioController/Create
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

        // GET: CalendarioController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CalendarioController/Edit/5
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

        // GET: CalendarioController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CalendarioController/Delete/5
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
