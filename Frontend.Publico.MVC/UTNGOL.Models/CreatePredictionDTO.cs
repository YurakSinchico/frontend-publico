using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTNGOL.Models
{
    public class CreatePredictionDTO
    {
        public int UserId { get; set; }

        public int MatchId { get; set; }

        public string PredictedResult { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public DateTime MatchStartDate { get; set; }
    }
}
