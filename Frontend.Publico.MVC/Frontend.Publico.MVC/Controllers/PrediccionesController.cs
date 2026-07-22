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

        public PrediccionesController(GolCoinConsumer golCoinConsumer)
        {
            _golCoinConsumer = golCoinConsumer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("IdUser");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var predictions =
                await _golCoinConsumer.ObtenerPrediccionesAsync(userId.Value);

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

                Console.WriteLine("======================");
                Console.WriteLine("USUARIO EN SESION:");
                Console.WriteLine(userId);
                Console.WriteLine("======================");


                if (userId == null)
                {
                    return Content("IdUser es NULL");
                }


                var wallet =
                    await _golCoinConsumer.ObtenerWalletAsync(userId.Value);


                if (wallet == null)
                {
                    Console.WriteLine("NO EXISTE WALLET PARA:");
                    Console.WriteLine(userId);

                    return Content(
                        "Wallet es NULL para usuario " + userId
                    );
                }


                Console.WriteLine("WALLET ENCONTRADA:");
                Console.WriteLine(wallet.Balance);


                var model = new PredictionViewModel
                {
                    MatchId = matchId,
                    HomeTeam = homeTeam,
                    AwayTeam = awayTeam,
                    MatchStartDate = matchStartDate,
                    WalletBalance = wallet.Balance
                };


                return View(model);
            }
            catch (Exception ex)
            {
                return Content(ex.ToString());
            }
        }

        // ============================
        // POST: Predicciones/Create
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
                TempData["Success"] =
                    "🎉 ¡Tu predicción fue registrada exitosamente!";

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