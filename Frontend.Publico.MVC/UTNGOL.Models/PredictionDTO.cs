using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTNGOL.Models
{
    public class PredictionDTO
    {
        public int Id { get; set; }

        public int MatchId { get; set; }

        public string PredictedResult { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public decimal AppliedOdds { get; set; }

        public string Status { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }
    }
}
