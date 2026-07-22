using Api.Consumer.Consumers;
using Frontend.Publico.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UTNGOL.Models;

namespace Frontend.Publico.MVC.Controllers
{
    [Authorize]
    public class BilleteraController : Controller
    {
        private readonly GolCoinConsumer _golCoinConsumer;

        public BilleteraController(GolCoinConsumer golCoinConsumer)
        {
            _golCoinConsumer = golCoinConsumer;
        }

        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("IdUser") 
                          ?? HttpContext.Session.GetInt32("UserId");

            if (userId == null && User.Identity?.IsAuthenticated == true)
            {
                var claim = User.FindFirst("IdUser") ?? User.FindFirst(ClaimTypes.NameIdentifier);
                if (claim != null && int.TryParse(claim.Value, out int idParsed))
                {
                    userId = idParsed;
                }
            }

            if (userId == null)
            {
                TempData["Error"] = "Debe iniciar sesión para acceder a su Billetera.";
                return RedirectToAction("Index", "Home");
            }

            WalletDTO? wallet = null;
            List<TransactionDTO> transactions = new List<TransactionDTO>();
            List<PredictionDTO> predictions = new List<PredictionDTO>();

            try
            {
                wallet = await _golCoinConsumer.ObtenerWalletAsync(userId.Value);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar Wallet: " + ex.Message);
            }

            try
            {
                var txs = await _golCoinConsumer.ObtenerTransaccionesAsync(userId.Value);
                if (txs != null) transactions = txs;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar Transacciones: " + ex.Message);
            }

            try
            {
                var preds = await _golCoinConsumer.ObtenerPrediccionesAsync(userId.Value);
                if (preds != null) predictions = preds;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al consultar Predicciones: " + ex.Message);
            }

            var model = new DashboardViewModel
            {
                Wallet = wallet ?? new WalletDTO { Balance = 10.00m },
                Transactions = transactions,
                Predictions = predictions
            };

            return View(model);
        }
    }
}