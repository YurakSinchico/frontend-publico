using System;

namespace UTNGOL.Models
{
    public class CreatePredictionDTO
    {
        public int UserId { get; set; }

        public int MatchId { get; set; }

        public string PredictedResult { get; set; } = "";

        public decimal Amount { get; set; }

        public DateTime MatchStartDate { get; set; }
        public decimal AppliedOdds { get; set; }
    }
}