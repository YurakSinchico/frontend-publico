using Api.Consumer.Consumers;
using Microsoft.AspNetCore.Mvc;
using UTNGOL.Models;

namespace Frontend.Publico.MVC.Controllers
{
    public class GruposController : Controller
    {
        private readonly EstadisticasConsumer _consumer;

        public GruposController(EstadisticasConsumer consumer)
        {
            _consumer = consumer;
        }

        // GET: Grupos
        public async Task<IActionResult> Index()
        {
            try
            {
                var selecciones = await _consumer.ObtenerSeleccionesAsync();

                var gruposAgrupados = selecciones
                    .GroupBy(s => s.Grupo)
                    .OrderBy(g => g.Key);

                return View(gruposAgrupados);
            }
            catch
            {
                return View(Enumerable.Empty<IGrouping<string, SeleccionDTO>>());
            }
        }

        // GET: Grupos/Details/A
        public async Task<IActionResult> Details(string id)
        {
            try
            {
                var selecciones = await _consumer.ObtenerSeleccionesAsync();
                var grupoKey = string.IsNullOrEmpty(id) ? "A" : id.Trim().ToUpper();

                var grupo = selecciones
                    .Where(s => (s.Grupo ?? "").Equals(grupoKey, StringComparison.OrdinalIgnoreCase))
                    .OrderByDescending(s => s.Puntos);

                ViewBag.GrupoKey = grupoKey;
                return View(grupo);
            }
            catch
            {
                ViewBag.GrupoKey = id ?? "A";
                return View(Enumerable.Empty<SeleccionDTO>());
            }
        }

        // GET: Grupos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Grupos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            return RedirectToAction(nameof(Index));
        }

        // GET: Grupos/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Grupos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            return RedirectToAction(nameof(Index));
        }

        // GET: Grupos/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Grupos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}