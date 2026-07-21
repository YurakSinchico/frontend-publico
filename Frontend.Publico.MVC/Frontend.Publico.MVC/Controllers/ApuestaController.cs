using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Api.Consumer.Consumers;
using UTNGOL.Models;

namespace Frontend.Publico.MVC.Controllers
{
    [Authorize]
    public class ApuestaController : Controller
    {
        private readonly EstadisticasConsumer _service;

        public ApuestaController(EstadisticasConsumer service)
        {
            _service = service;
        }

        // GET: ApuestaController/Create
        public IActionResult Create(int id)
        {
            return View(new PrediccionDTO
            {
                IdPartido = id
            });
        }

        // POST: ApuestaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrediccionDTO prediccion)
        {
            try
            {
                string email = User.Identity?.Name;
                string password = HttpContext.Session.GetString("Password");

                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    return RedirectToAction("Login", "Account");
                }

                var response = await _service.GuardarPrediccionAsync(prediccion, email, password);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Partidos");
                }

                ModelState.AddModelError("", "Error al guardar la predicción.");
                return View(prediccion);
            }
            catch
            {
                ModelState.AddModelError("", "Error de conexión con el servidor.");
                return View(prediccion);
            }
        }
    }
}