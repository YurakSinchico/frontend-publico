using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using UTNGOL.Servicios.DTOs;
using System.Net.Http;

namespace Frontend.Publico.MVC.Controllers
{
    public class PartidosController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                using var client = new HttpClient();
                // Usamos la IP de tu servidor
                var url = "http://192.168.100.138:8080/estadisticas-backend/api/partidos";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                    var partidos = JsonSerializer.Deserialize<List<PartidoDTO>>(json, options);

                    return View(partidos);
                }
                else
                {
                    // Si el servidor responde un error, veremos el código aquí
                    return Content("Error del servidor: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                // Si la conexión falla (Firewall, IP mal, etc), veremos esto
                return Content("Error de conexión: " + ex.Message);
            }
        }
    }
}