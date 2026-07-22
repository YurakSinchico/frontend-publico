using Api.Consumer.Consumers;
using Frontend.Publico.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UTNGOL.Models;

namespace Frontend.Publico.MVC.Controllers
{
    [Authorize]
    public class PrediccionesController : Controller
    {
        private readonly GolCoinConsumer _golCoinConsumer;
        private readonly EstadisticasConsumer _estadisticasConsumer;

        public PrediccionesController(GolCoinConsumer golCoinConsumer, EstadisticasConsumer estadisticasConsumer)
        {
            _golCoinConsumer = golCoinConsumer;
            _estadisticasConsumer = estadisticasConsumer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("IdUser");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var predictions = await _golCoinConsumer.ObtenerPrediccionesAsync(userId.Value);

            return View(predictions);
        }

        // ============================
        // GET: Predicciones/Create
        // ============================
        [HttpGet]
        public async Task<IActionResult> Create(
            int matchId,
            string homeTeam,
            string awayTeam,
            DateTime matchStartDate)
        {
            try
            {
                int? userId = HttpContext.Session.GetInt32("IdUser");

                if (userId == null)
                {
                    return RedirectToAction("Index", "Home");
                }

                // 1. Obtener Wallet (fallback a 10.00 GolCoins si no se ha instanciado)
                decimal balance = 10.00m;
                try
                {
                    var wallet = await _golCoinConsumer.ObtenerWalletAsync(userId.Value);
                    if (wallet != null)
                    {
                        balance = wallet.Balance;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener wallet: " + ex.Message);
                }

                // 2. Obtener lista real de partidos para el selector dinámico
                var partidos = new List<PartidoDTO>();
                try
                {
                    var listaPartidos = await _estadisticasConsumer.ObtenerPartidosAsync();
                    if (listaPartidos != null && listaPartidos.Any())
                    {
                        partidos = listaPartidos.ToList();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener partidos: " + ex.Message);
                }

                // Seleccionar partido objetivo
                var partidoActual = partidos.FirstOrDefault(p => p.IdPartido == matchId) 
                    ?? partidos.FirstOrDefault() 
                    ?? new PartidoDTO { IdPartido = matchId > 0 ? matchId : 1, SeleccionLocal = "México", SeleccionVisitante = "USA" };

                var model = new PredictionViewModel
                {
                    MatchId = partidoActual.IdPartido,
                    HomeTeam = !string.IsNullOrEmpty(homeTeam) ? homeTeam : partidoActual.SeleccionLocal,
                    AwayTeam = !string.IsNullOrEmpty(awayTeam) ? awayTeam : partidoActual.SeleccionVisitante,
                    MatchStartDate = matchStartDate != default ? matchStartDate : DateTime.Now.AddDays(1),
                    WalletBalance = balance
                };

                ViewBag.Partidos = partidos;

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en Predicciones/Create: " + ex);
                return View(new PredictionViewModel
                {
                    MatchId = 1,
                    HomeTeam = "México",
                    AwayTeam = "USA",
                    MatchStartDate = DateTime.Now.AddDays(1),
                    WalletBalance = 10.00m
                });
            }
        }

        // ============================
        // POST: Predicciones/Create
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PredictionViewModel model)
        {
            int? userId = HttpContext.Session.GetInt32("IdUser");
            if (userId == null)
                return RedirectToAction("Index", "Home");

            try
            {
                var dto = new CreatePredictionDTO
                {
                    UserId = userId.Value,
                    MatchId = model.MatchId > 0 ? model.MatchId : 1,
                    PredictedResult = !string.IsNullOrEmpty(model.PredictedResult) ? model.PredictedResult : "1",
                    Amount = model.Amount > 0 ? model.Amount : 1,
                    MatchStartDate = model.MatchStartDate != default ? model.MatchStartDate : DateTime.Now.AddDays(1)
                };

                var response = await _golCoinConsumer.CrearPrediccionAsync(dto);

                if (response.IsSuccessStatusCode)
                {
                    TempData["Success"] = "🎉 ¡Tu predicción fue registrada exitosamente!";
                    return RedirectToAction("Index", "Dashboard");
                }

                var mensaje = await response.Content.ReadAsStringAsync();

                // Recargar información en caso de error
                try
                {
                    var wallet = await _golCoinConsumer.ObtenerWalletAsync(userId.Value);
                    if (wallet != null) model.WalletBalance = wallet.Balance;
                }
                catch { model.WalletBalance = 10.00m; }

                try
                {
                    ViewBag.Partidos = await _estadisticasConsumer.ObtenerPartidosAsync();
                }
                catch { }

                ModelState.AddModelError("", !string.IsNullOrEmpty(mensaje) ? mensaje : "No se pudo registrar la predicción en el servidor.");

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Excepción en PrediccionesController POST Create: " + ex);

                model.WalletBalance = 10.00m;
                try
                {
                    ViewBag.Partidos = await _estadisticasConsumer.ObtenerPartidosAsync();
                }
                catch { }

                ModelState.AddModelError("", "Ocurrió un inconveniente al comunicarse con la API de GolCoin (192.168.100.118:7182). Verifique la conectividad del microservicio.");

                return View(model);
            }
        }
    }
}
