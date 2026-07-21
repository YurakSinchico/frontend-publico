using UTNGOL.Models;

namespace Frontend.Publico.MVC.ViewModels
{
    public class WalletViewModel
    {
        public WalletDTO? Wallet { get; set; }

        public List<TransactionDTO> Transactions { get; set; } = new();

        public List<PredictionDTO> Predictions { get; set; } = new();
    }
}