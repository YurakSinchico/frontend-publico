using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UTNGOL.Servicios.DTOs;
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
            // Asegúrate de que esta URL sea exactamente la IP de tu Fedora (192.168.100.138)
            var url = "http://192.168.100.138:8080/estadisticas-backend/api/partidos";

            using var client = new HttpClient();
            try
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    // Esto deserializa el JSON en tu lista de objetos DTO
                    var partidos = JsonSerializer.Deserialize<List<PartidoDTO>>(json);
                    return View(partidos);
                }
            }
            catch (Exception ex)
            {
                // Si hay error de conexión, imprime en la consola de Visual Studio
                Console.WriteLine("Error al conectar con la API: " + ex.Message);
            }

            return View(new List<PartidoDTO>()); // Retorna lista vacía si hay error
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
