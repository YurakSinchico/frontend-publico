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
            // Temporalmente usamos el usuario 1
            // Después lo obtendremos automáticamente desde el login.
            int userId = 1;

            var model = new DashboardViewModel
            {
                Wallet = await _golCoinConsumer.ObtenerWalletAsync(userId),
                Predictions = await _golCoinConsumer.ObtenerPrediccionesAsync(userId),
                Transactions = await _golCoinConsumer.ObtenerTransaccionesAsync(userId)
            };

            return View(model);
        }
    }
}