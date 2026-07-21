namespace UTNGOL.Models
{
    public class TransactionDTO
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; } = string.Empty;

        public int? PredictionId { get; set; }

        public DateTime Date { get; set; }
    }
}