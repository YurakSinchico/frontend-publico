using System;
using System.ComponentModel.DataAnnotations;

namespace Frontend.Publico.MVC.ViewModels
{
    public class PredictionViewModel
    {
        public int UserId { get; set; }

        public int MatchId { get; set; }

        public string HomeTeam { get; set; } = string.Empty;

        public string AwayTeam { get; set; } = string.Empty;

        public DateTime MatchStartDate { get; set; }

        public decimal WalletBalance { get; set; }

        [Required(ErrorMessage = "Seleccione un resultado.")]
        public string PredictedResult { get; set; } = string.Empty;

        [Required(ErrorMessage = "Ingrese un monto.")]
        [Range(1, 100000, ErrorMessage = "Monto inválido.")]
        public decimal Amount { get; set; }
    }
}