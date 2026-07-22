using Api.Consumer.Consumers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Frontend.Publico.MVC.Controllers
{
    [Authorize]
    public class RankingController : Controller
    {
        private readonly GolCoinConsumer _golCoinConsumer;

        public RankingController(GolCoinConsumer golCoinConsumer)
        {
            _golCoinConsumer = golCoinConsumer;
        }

        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("IdUser");
            if (userId.HasValue)
            {
                var wallet = await _golCoinConsumer.ObtenerWalletAsync(userId.Value);
                ViewBag.GolCoins = wallet?.Balance ?? 10;
            }

            var ranking = new List<RankingItemViewModel>();

            try
            {
                // Consumir directamente las wallets reales de la API Backend de GolCoin
                var walletsReales = await _golCoinConsumer.ObtenerTodasLasWalletsAsync();
                if (walletsReales != null && walletsReales.Any())
                {
                    int pos = 1;
                    foreach (var w in walletsReales.OrderByDescending(x => x.Balance))
                    {
                        var preds = await _golCoinConsumer.ObtenerPrediccionesAsync(w.UserId);
                        int aciertos = preds.Count(p => p.Status == "Ganada");

                        ranking.Add(new RankingItemViewModel
                        {
                            Rank = pos++,
                            Nombre = $"Usuario #{w.UserId}",
                            Aciertos = aciertos,
                            Saldo = w.Balance
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar ranking desde API backend: " + ex.Message);
            }

           
            if (!ranking.Any())
            {
                ranking = new List<RankingItemViewModel>
                {
                    new RankingItemViewModel { Rank = 1, Nombre = "Usuario Actual", Aciertos = 0, Saldo = ViewBag.GolCoins ?? 10.00m }
                };
            }

            return View(ranking);
        }
    }

    public class RankingItemViewModel
    {
        public int Rank { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public int Aciertos { get; set; }
        public decimal Saldo { get; set; }
    }
}

