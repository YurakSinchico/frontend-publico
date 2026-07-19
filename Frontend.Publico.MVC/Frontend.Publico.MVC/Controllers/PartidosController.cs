using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UTNGOL.Servicios.DTOs;
using System.Net.Http;

namespace Frontend.Publico.MVC.Controllers
{
    public class PartidosController : Controller
    {
        // Definimos la URL base para no repetirla
        private readonly string _baseUrl = "http://192.168.100.138:8080/estadisticas-backend/api/partidos";

        public async Task<IActionResult> Index()
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync(_baseUrl);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var partidos = JsonSerializer.Deserialize<List<PartidoDTO>>(json, options);
                    return View(partidos);
                }
                return Content("Error del servidor: " + response.StatusCode);
            }
            catch (Exception ex)
            {
                return Content("Error de conexión: " + ex.Message);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                using var client = new HttpClient();
                // Construimos la URL completa para el detalle
                var response = await client.GetAsync($"{_baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var partido = JsonSerializer.Deserialize<PartidoDTO>(json, options);
                    return View(partido);
                }
                return NotFound("Partido no encontrado.");
            }
            catch (Exception ex)
            {
                return Content("Error de conexión: " + ex.Message);
            }
        }
    }
}