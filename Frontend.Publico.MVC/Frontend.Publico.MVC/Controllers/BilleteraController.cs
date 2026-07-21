using Api.Consumer.Consumers;
using Frontend.Publico.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Publico.MVC.Controllers
{
    [Authorize(Roles = "Registrado,Administrador")]
    public class BilleteraController : Controller
    {
        private readonly GolCoinConsumer _golCoinConsumer;

        public BilleteraController(GolCoinConsumer golCoinConsumer)
        {
            _golCoinConsumer = golCoinConsumer;
        }

        public async Task<IActionResult> Index()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");

            if (userId == null)
                return RedirectToAction("Login", "Account");

            var model = new DashboardViewModel
            {
                Wallet = await _golCoinConsumer.ObtenerWalletAsync(userId.Value),
                Transactions = await _golCoinConsumer.ObtenerTransaccionesAsync(userId.Value),
                Predictions = await _golCoinConsumer.ObtenerPrediccionesAsync(userId.Value)
            };

            return View(model);
        }
    }
}