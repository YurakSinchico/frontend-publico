using UTNGOL.Models;

namespace Frontend.Publico.MVC.ViewModels
{
    public class DashboardViewModel
    {
        public WalletDTO? Wallet { get; set; }

        public List<PredictionDTO> Predictions { get; set; } = new();

        public List<TransactionDTO> Transactions { get; set; } = new();

        public int TotalPredictions => Predictions.Count;

        public int WonPredictions =>
            Predictions.Count(p => p.Status == "Ganada");

        public int LostPredictions =>
            Predictions.Count(p => p.Status == "Perdida");

        public int PendingPredictions =>
            Predictions.Count(p => p.Status == "Pendiente");
    }
}