using Api.Consumer.Consumers;
using Microsoft.AspNetCore.Mvc;
using UTNGOL.Models;

namespace Frontend.Publico.MVC.Controllers
{
    public class CalendarioController : Controller
    {
        private readonly EstadisticasConsumer _consumer;

        public CalendarioController(EstadisticasConsumer consumer)
        {
            _consumer = consumer;
        }

        // GET: Calendario
        public async Task<IActionResult> Index()
        {
            try
            {
                var partidos = await _consumer.ObtenerPartidosAsync();
                return View(partidos);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al conectar con la API: " + ex.Message);
                return View(new List<PartidoDTO>());
            }
        }

        // GET: Calendario/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: Calendario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Calendario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
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

        // GET: Calendario/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: Calendario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
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

        // GET: Calendario/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: Calendario/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
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