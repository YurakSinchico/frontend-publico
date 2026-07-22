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
            // Obtener usuario de la sesión
            var userId = HttpContext.Session.GetInt32("IdUser");


            if (userId == null)
            {
                return RedirectToAction(
                    "Login",
                    "Account"
                );
            }


            Console.WriteLine("======================");
            Console.WriteLine("DASHBOARD");
            Console.WriteLine("Usuario:");
            Console.WriteLine(userId);
            Console.WriteLine("======================");


            // Obtener wallet del usuario
            var wallet =
                await _golCoinConsumer
                .ObtenerWalletAsync(userId.Value);



            if (wallet == null)
            {
                Console.WriteLine(
                    "NO EXISTE WALLET PARA USUARIO: "
                    + userId
                );
            }
            else
            {
                Console.WriteLine(
                    "GOLCOINS: "
                    + wallet.Balance
                );
            }



            var model = new DashboardViewModel
            {
                Wallet = wallet,

                Predictions =
                    await _golCoinConsumer
                    .ObtenerPrediccionesAsync(userId.Value),

                Transactions =
                    await _golCoinConsumer
                    .ObtenerTransaccionesAsync(userId.Value)
            };


            // Para mostrar directamente en la vista
            ViewBag.GolCoins =
                wallet?.Balance ?? 0;



            return View(model);
        }
    }
}