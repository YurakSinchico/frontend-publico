using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UTNGOL.Models
{
    public class SettlePredictionDTO
    {
        public int MatchId { get; set; }

        public string OfficialResult { get; set; } = string.Empty;

        public decimal AppliedOdds { get; set; }
    }
}
