using Api.Consumer.Consumers;
using Microsoft.AspNetCore.Mvc;
using UTNGOL.Models;

namespace Frontend.Publico.MVC.Controllers
{
    public class EstadisticasController : Controller
    {
        private readonly EstadisticasConsumer _consumer;

        public EstadisticasController(EstadisticasConsumer consumer)
        {
            _consumer = consumer;
        }

        // GET: Estadisticas
        public async Task<IActionResult> Index(int id = 1)
        {
            try
            {
                GrupoDTO grupo = await _consumer.ObtenerTablaPosicionesAsync(id);

                if (grupo == null || grupo.Posiciones == null)
                {
                    return View(new List<PosicionDTO>());
                }

                return View(grupo.Posiciones);
            }
            catch
            {
                return View(new List<PosicionDTO>());
            }
        }

        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}