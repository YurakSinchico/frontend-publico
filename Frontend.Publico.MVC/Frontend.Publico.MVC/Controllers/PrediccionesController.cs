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
            if (!ModelState.IsValid)
            {
                int? id = HttpContext.Session.GetInt32("IdUser");

                if (id != null)
                {
                    var wallet = await _golCoinConsumer.ObtenerWalletAsync(id.Value);

                    if (wallet != null)
                        model.WalletBalance = wallet.Balance;
                }

                return View(model);
            }

            int? userId = HttpContext.Session.GetInt32("IdUser");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var dto = new CreatePredictionDTO
            {
                UserId = userId.Value,
                MatchId = model.MatchId,
                PredictedResult = model.PredictedResult,
                Amount = model.Amount,
                MatchStartDate = model.MatchStartDate
            };

            var response = await _golCoinConsumer.CrearPrediccionAsync(dto);

            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = "🎉 ¡Tu predicción fue registrada exitosamente!";
                return RedirectToAction("Index", "Dashboard");
            }

            var mensaje = await response.Content.ReadAsStringAsync();

            var billetera = await _golCoinConsumer.ObtenerWalletAsync(userId.Value);

            if (billetera != null)
                model.WalletBalance = billetera.Balance;

            ModelState.AddModelError("", mensaje);

            return View(model);
        }
    }
}
