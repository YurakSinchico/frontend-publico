using Api.Consumer.Consumers;
using Frontend.Publico.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Publico.MVC.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly GolCoinConsumer _golCoinConsumer;

        public DashboardController(GolCoinConsumer golCoinConsumer)
        {
            _golCoinConsumer = golCoinConsumer;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener el usuario que inició sesión
            var userId = HttpContext.Session.GetInt32("IdUser");

            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var model = new DashboardViewModel
            {
                Wallet = await _golCoinConsumer.ObtenerWalletAsync(userId.Value),
                Predictions = await _golCoinConsumer.ObtenerPrediccionesAsync(userId.Value),
                Transactions = await _golCoinConsumer.ObtenerTransaccionesAsync(userId.Value)
            };

            return View(model);
        }
    }
}