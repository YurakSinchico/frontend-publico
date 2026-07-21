using UTNGOL.Models;

namespace Frontend.Publico.MVC.ViewModels
{
    public class DashboardViewModel
    {
        public WalletDTO? Wallet { get; set; }

        public List<PredictionDTO> Predictions { get; set; } = new();

        public List<TransactionDTO> Transactions { get; set; } = new();
    }
}